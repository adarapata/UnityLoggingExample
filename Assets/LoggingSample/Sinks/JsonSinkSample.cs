using Unity.Logging;
using Unity.Logging.Sinks;
using UnityEngine;

namespace LoggingSample.Sinks
{
    public class JsonSinkSample : MonoBehaviour
    {
        private void Start()
        {
            var config = new LoggerConfig()
                .WriteTo.JsonFile("log.json");
            Log.Logger = new Unity.Logging.Logger(config);
            Log.Info("Log Info {foo},{bar}", 10, "aaa");
        }
    }
}