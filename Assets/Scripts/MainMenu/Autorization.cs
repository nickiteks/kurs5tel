using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Database;

public class Autorization : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Login;
    [SerializeField]
    private TMP_Text Password;
    public void ButtonAutorization_Click()
    {
        try
        {
            Client.Instance.user = APIClient.GetRequest<User>($"api/Client/Login?login={Login.text}&password={Password.text}");
        } catch
        {
            Debug.Log("Неправильный логин или пароль");
        }        
    }
}
