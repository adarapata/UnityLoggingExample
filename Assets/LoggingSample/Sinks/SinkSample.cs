using Unity.Logging;
using Unity.Logging.Sinks;
using UnityEngine;

namespace LoggingSample.Sinks
{
    public class SinkSample : MonoBehaviour
    {
        private void Start()
        {
            var config = new LoggerConfig()
                .WriteTo.File("LogName.log", minLevel: LogLevel.Debug)
                .WriteTo.JsonFile("log.json", minLevel: LogLevel.Warning)
                .WriteTo.UnityEditorConsole();
            Log.Logger = new Unity.Logging.Logger(config);
        }
    }
}