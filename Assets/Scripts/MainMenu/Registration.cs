using Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Registration : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Login;
    [SerializeField]
    private TMP_Text Password;
    [SerializeField]
    private TMP_Text NickName;
    public void ButtonRegister_Click()
    {
        APIClient.PostRequest("api/Client/Register", new User
        {
            Login = Login.text,
            Password = Password.text,
            Nickname = NickName.text,
        });
    }
}
