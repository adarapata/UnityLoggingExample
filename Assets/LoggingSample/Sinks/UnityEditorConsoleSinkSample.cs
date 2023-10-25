using Unity.Logging;
using Unity.Logging.Sinks;
using UnityEngine;

namespace LoggingSample.Sinks
{
    public class UnityEditorConsoleSinkSample : MonoBehaviour
    {
        private void Start()
        {
            var config = new LoggerConfig()
                .WriteTo.UnityEditorConsole();
            Log.Logger = new Unity.Logging.Logger(config);
            Log.Info("ログをUnityEditorConsoleに出力します");
        }
    }
}