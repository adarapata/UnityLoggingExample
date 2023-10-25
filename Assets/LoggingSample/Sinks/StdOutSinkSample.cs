using Unity.Logging;
using Unity.Logging.Sinks;
using UnityEngine;

namespace LoggingSample.Sinks
{
    public class StdOutSinkSample : MonoBehaviour
    {
        private void Start()
        {
            var config = new LoggerConfig()
                .WriteTo.StdOut();
            Log.Logger = new Unity.Logging.Logger(config);
            Log.Info("ログを標準出力に出力します");
        }
    }
}