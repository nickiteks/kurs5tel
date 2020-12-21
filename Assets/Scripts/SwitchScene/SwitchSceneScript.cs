using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneScript : MonoBehaviour
{
    /// <summary>
    /// Название сцены, на которую надо перейти
    /// </summary>
    [SerializeField]
    private string nameSwitchLocation;

    private void LoadLocation()
    {
        SceneManager.LoadScene(nameSwitchLocation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LoadLocation();
    }
}
