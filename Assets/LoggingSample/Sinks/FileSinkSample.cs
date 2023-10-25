using Unity.Logging;
using Unity.Logging.Sinks;
using UnityEngine;

namespace LoggingSample.Sinks
{
    public class FileSinkSample : MonoBehaviour
    {
        private void Start()
        {
            var config = new LoggerConfig()
                .WriteTo.File("LogName.log");
            Log.Logger = new Unity.Logging.Logger(config);
            Log.Info("ログをファイルに出力します");
        }
    }
}