using Unity.Logging;
using Unity.Logging.Sinks;
using UnityEngine;

namespace LoggingSample
{
    public class RedirectUnityLogsSample : MonoBehaviour
    {
        private void Start()
        {
            var config = new LoggerConfig()
                .RedirectUnityLogs()
                .WriteTo.UnityEditorConsole();
            Log.Logger = new Unity.Logging.Logger(config);

            if (config.GetRedirectUnityLogs())
            {
                Log.Info("RedirectUnityLogsが有効です");
            }
            else
            {
                Log.Info("RedirectUnityLogsが無効です");
            }
        }
    }
}