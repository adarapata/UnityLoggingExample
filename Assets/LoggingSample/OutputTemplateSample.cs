using Unity.Logging;
using Unity.Logging.Sinks;
using UnityEngine;

namespace LoggingSample
{
    public class OutputTemplateSample : MonoBehaviour
    {
        private void Start()
        {
            var config = new LoggerConfig()
                .OutputTemplate("{Timestamp} - {Level} - {Message}")
                .WriteTo.UnityEditorConsole();
            Log.Logger = new Unity.Logging.Logger(config);  // Templateを変更
            Log.Info("Test");
            // => 2023/09/27 12:00:00.000 - INFO - Test
        }
    }
}