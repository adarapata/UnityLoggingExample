using Unity.Logging;
using Unity.Logging.Sinks;
using UnityEngine;

namespace LoggingSample.Sinks
{
    public class CustomSinkSample : MonoBehaviour
    {
        private void Start()
        {
            var config = new LoggerConfig()
                .SyncMode.FullSync()           // 非同期だとすぐに出力されないので同期
                .WriteTo.UnityEditorConsole()  // 動作確認のためにUnityEditorConsoleにも出力
                .WriteTo.CountLogger();
            Log.Logger = new Unity.Logging.Logger(config);
            Log.Info("foo");
            Log.Info("bar");
            Log.Info(CountSinkSystem.LogCounter); // => 2
        }
    }
}