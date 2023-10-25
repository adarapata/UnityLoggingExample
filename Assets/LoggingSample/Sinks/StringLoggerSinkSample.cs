using Unity.Logging;
using Unity.Logging.Sinks;
using UnityEngine;

namespace LoggingSample.Sinks
{
    public class StringLoggerSinkSample : MonoBehaviour
    {
        private void Start()
        {
            var config = new LoggerConfig()
                .SyncMode.FullSync()
                .WriteTo.UnityEditorConsole()  // 動作確認のためにUnityEditorConsoleにも出力する
                .WriteTo.StringLogger();
            Log.Logger = new Unity.Logging.Logger(config);

            Log.Info("foo");
            Log.Info("bar");

            var stringSink = Log.Logger.GetSink<StringSink>();
            Log.Info(stringSink.GetString()); 
            // 2023/10/23 15:21:47.098 | INFO | foo
            // 2023/10/23 15:21:47.107 | INFO | bar
        }
    }
}