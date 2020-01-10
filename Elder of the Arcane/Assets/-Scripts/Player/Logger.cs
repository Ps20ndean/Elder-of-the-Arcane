using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Logger : MonoBehaviour
{
    // Start is called before the first frame update
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
