using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavePositionScript : MonoBehaviour
{
    public Transform[] player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Saver();        
    }
    private void Start()
    {
        Debug.Log(gameObject.name);
        if (PlayerPrefs.HasKey("Position"))
        {
            Loader();
            PlayerPrefs.DeleteKey("Position");
            
            Destroy(gameObject);
        }
    }
    public void Saver()
    {
        SaveData data = new SaveData();        
        data = new SaveData() { PosX = player[0].position.x, PosY = player[0].position.y };        
        string json = JsonUtility.ToJson(data);       
        PlayerPrefs.SetString("Position", json);
    }
    public void Loader()
    {
        SaveData save = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("Position"));
        Vector2 data = new Vector2(save.PosX, save.PosY);
        for (int i = 0; i < player.Length; i++)
        {
           player[i].position = data;
        }  

    }  
}
[Serializable]
public class SaveData 
{
    public float PosX;
    public float PosY;
}

