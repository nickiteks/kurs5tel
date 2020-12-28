using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIConfig : Singleton<APIConfig>
{
    public readonly string ServerIp = "127.0.0.1";
    public readonly string ServerPort = "5001";

    public string url()
    {
        return string.Format("https://{0}:{1}/api/", ServerIp, ServerPort);
    }
}
