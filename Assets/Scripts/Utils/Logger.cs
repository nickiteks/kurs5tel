using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Logger 
{
    private readonly string filePath;
    public Logger(string fileName)
    {
        filePath = Application.streamingAssetsPath + "/Logs/" + fileName + ".txt";
        FileInfo fileInfo = new FileInfo(filePath);
        fileInfo.Directory.Create();
        
    }

    public void Log(string info)
    {
        StreamWriter sw = new StreamWriter(filePath, true);
        sw.WriteLine(info);
        sw.Close();

        Debug.Log(filePath);
    }
}
