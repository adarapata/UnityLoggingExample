using Unity.Logging;
using Unity.Logging.Sinks;
using UnityEngine;

namespace LoggingSample
{
    public class CaptureStackTraceSample : MonoBehaviour
    {
        private void Start()
        {
            var config = new LoggerConfig()
                .CaptureStacktrace()
                .WriteTo.UnityEditorConsole();
            Log.Logger = new Unity.Logging.Logger(config);

            if (config.GetCaptureStacktrace())
            {
                Log.Info("CaptureStacktraceが有効です");
            }
            else
            {
                Log.Info("CaptureStacktraceが無効です");
            }
        }
    }
}