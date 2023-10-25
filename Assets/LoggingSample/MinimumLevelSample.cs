using Unity.Logging;
using Unity.Logging.Sinks;
using UnityEngine;

namespace LoggingSample
{
    public class MinimumLevelSample : MonoBehaviour
    {
        private void Start()
        {
            var config = new LoggerConfig()
                .MinimumLevel.Info()
                .WriteTo.UnityEditorConsole();
            Log.Logger = new Unity.Logging.Logger(config);  // ログレベルをInfoに変更
            Log.Debug("Debug");                         // 出力されない
        }
    }
}