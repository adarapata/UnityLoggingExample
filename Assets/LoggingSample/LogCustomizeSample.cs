using Unity.Logging;
using UnityEngine;

namespace LoggingSample
{
    public class LogCustomizeSample : MonoBehaviour
    {
        private void Start()
        {
            var config = new LoggerConfig()
                .SyncMode.FatalIsSync()
                .MinimumLevel.Debug()
                .OutputTemplate("{Timestamp} - {Level} - {Message}")
                .CaptureStacktrace()
                .RedirectUnityLogs();

            Log.Logger = new Unity.Logging.Logger(config);
        }
    }
}