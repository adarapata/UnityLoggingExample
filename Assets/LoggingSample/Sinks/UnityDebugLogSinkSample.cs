using Unity.Logging;
using Unity.Logging.Sinks;
using UnityEngine;

namespace LoggingSample.Sinks
{
    public class UnityDebugLogSinkSample : MonoBehaviour
    {
        private void Start()
        {
            var config = new LoggerConfig()
                .WriteTo.UnityDebugLog();
            Log.Logger = new Unity.Logging.Logger(config);
            Log.Info("ログをConsoleに出力します");
        }
    }
}