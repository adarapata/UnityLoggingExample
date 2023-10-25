using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Logging;
using Unity.Logging.Sinks;


namespace LoggingSample.Sinks
{
    public static class CountLoggerExtensions
    {
        // WriteTo.CountLogger()のように呼び出せるようにするための拡張メソッド
        public static LoggerConfig CountLogger(this LoggerWriterConfig writeTo, LogLevel? minLevel = null)
        {
            return writeTo.AddSinkConfig(new CountSinkSystem.Configure(writeTo, default, false, minLevel));
        }
    }

    // SinkSystemBaseを継承する場合BurstCompile対応が必須になる
    [BurstCompile]
    public class CountSinkSystem : SinkSystemBase
    {
        public static int LogCounter;

        public class Configure : SinkConfiguration
        {
            public Configure(LoggerWriterConfig writeTo, FormatterStruct formatter, bool? captureStackTraceOverride = null, LogLevel? minLevelOverride = null, FixedString512Bytes? outputTemplateOverride = null)
                : base(writeTo, formatter, captureStackTraceOverride, minLevelOverride, outputTemplateOverride)
            {
            }

            public override SinkSystemBase CreateSinkInstance(Logger logger)
            {
                // CountSinkSystemのインスタンスを作成する
                return CreateAndInitializeSinkInstance<CountSinkSystem>(logger, this);
            }
        }

        public override LogController.SinkStruct ToSinkStruct()
        {
            var s = base.ToSinkStruct();
            // ログ取得時のデリゲートを設定する
            s.OnLogMessageEmit = new OnLogMessageEmitDelegate(OnLogMessageEmitFunc);
            return s;
        }

        public override void Initialize(Logger logger, SinkConfiguration systemConfig)
        {
            // インスタンスの初期化時にログのカウント回数も初期化する
            CountLogWrapper.Initialize();
            base.Initialize(logger, systemConfig);
        }

        /// <summary>
        /// ログメッセージが発行されたときに呼ばれるコールバック
        /// </summary>
        [BurstCompile]
        [AOT.MonoPInvokeCallback(typeof(OnLogMessageEmitDelegate.Delegate))]
        internal static void OnLogMessageEmitFunc(in LogMessage logEvent, ref FixedString512Bytes outTemplate, ref UnsafeText messageBuffer, IntPtr memoryManager, IntPtr userData, Allocator allocator)
        {
            try
            {
                // BurstCompileに対応させる場合readonlyではないstatic変数にアクセスできないため、
                // CountLogWrapperを経由してカウントする
                CountLogWrapper.Write();
            }
            finally
            {
                messageBuffer.Length = 0;
            }
        }
    }

    /// <summary>
    /// ログが出力された回数をカウントするクラス
    /// </summary>
    internal static class CountLogWrapper
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void WriteDelegate();

        private struct CountLogWrapperKey
        {
        }

        private static bool _initialized;

        internal static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
            // デリゲートを登録する
            Burst2ManagedCall<WriteDelegate, CountLogWrapperKey>.Init(WriteFunc);
        }

        [AOT.MonoPInvokeCallback(typeof(WriteDelegate))]
        private static void WriteFunc()
        {
            // カウント処理
            CountSinkSystem.LogCounter++;
        }

        public static void Write()
        {
            var ptr = Burst2ManagedCall<WriteDelegate, CountLogWrapperKey>.Ptr();
#if LOGGING_USE_UNMANAGED_DELEGATES
            ((delegate * unmanaged[Cdecl] <LogLevel, byte*, int, void>)ptr.Value)(level, data, length);
#else
            ptr.Invoke();
#endif
        }
    }

    /// <summary>
    /// BurstからManagedコードを呼び出すためのデリゲートを管理するクラス
    /// </summary>
    internal static class Burst2ManagedCall<T, Key>
    {
        private static T _delegate;
        private static readonly SharedStatic<FunctionPointer<T>> _sharedStatic = SharedStatic<FunctionPointer<T>>.GetOrCreate<FunctionPointer<T>, Key>(16);
        public static bool IsCreated => _sharedStatic.Data.IsCreated;

        public static void Init(T @delegate)
        {
            CheckIsNotCreated();
            _delegate = @delegate;
            _sharedStatic.Data = new FunctionPointer<T>(Marshal.GetFunctionPointerForDelegate(_delegate));
        }

        public static ref FunctionPointer<T> Ptr()
        {
            CheckIsCreated();
            return ref _sharedStatic.Data;
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS"), Conditional("UNITY_DOTS_DEBUG")]
        private static void CheckIsCreated()
        {
            if (IsCreated == false)
                throw new InvalidOperationException("Burst2ManagedCall was NOT created!");
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS"), Conditional("UNITY_DOTS_DEBUG")]
        private static void CheckIsNotCreated()
        {
            if (IsCreated)
                throw new InvalidOperationException("Burst2ManagedCall was already created!");
        }
    }
}
