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

    private void Start()
    {
        
    }

    public void LoadLocation()
    {
        SceneManager.LoadScene(nameSwitchLocation);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UIManager.Instance.OpenSwitchScenePanel();
        }
    }
}
