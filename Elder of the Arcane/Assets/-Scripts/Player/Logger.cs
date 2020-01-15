using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Logger : MonoBehaviour
{
    // this script writes incoming log messages to the log.txt file
    void Start()
    {
        DontDestroyOnLoad(this);
        Application.logMessageReceived += HandleLog;
    }
    void OnApplicationQuit()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        File.AppendAllText("Logs/Log.txt" , logString + stackTrace);
    }
}
