using Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : Singleton<Client>
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public User user;
    public bool isNewGame { get; private set; }

    public void NewGame()
    {
        isNewGame = true;
    }

    public void ContinueGame()
    {
        isNewGame = false;
    }


}
