using Unity.Logging;
using UnityEngine;

public class UnityLoggingTest : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Debug Log");
        Debug.LogWarning("Debug Warning");
        Debug.LogError("Debug Error");
        Log.Info("Log Info");
        Log.Warning("Log Warning");
        Log.Error("Log Error");
    }
}