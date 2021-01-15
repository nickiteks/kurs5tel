using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using Newtonsoft.Json;

public class APIClient : Singleton<APIClient>
{
    private static readonly HttpClient client = new HttpClient();

    private void Start()
    {
        Connect();
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public static void Connect()
    {
        client.BaseAddress = new Uri("https://savegame.conveyor.cloud");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public static T GetRequest<T>(string requestUrl)
    {
        var response = client.GetAsync(requestUrl);
        var result = response.Result.Content.ReadAsStringAsync().Result;

        if (response.Result.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<T>(result);
        }
        else
        {
            throw new Exception(result);
        }
    }

    public static void PostRequest<T>(string requestUrl, T model)
    {
        Debug.Log( client.BaseAddress+requestUrl);
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = client.PostAsync(requestUrl, data);
        var result = response.Result.Content.ReadAsStringAsync().Result;
        if (!response.Result.IsSuccessStatusCode)
        {
            Debug.Log(response);
            throw new Exception(result);
        }
    }
}
