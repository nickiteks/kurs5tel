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

    public void LoadLocation()
    {
        SceneManager.LoadScene(nameSwitchLocation);
        if (SwitchSceneValidation.HasScene(nameSwitchLocation)) SceneManager.LoadScene(nameSwitchLocation);
        else Debug.Log(SceneNotFoundException.SceneNotFound(nameSwitchLocation));
    }

    public void LoadLocation(string nameLocation)
    { 
        SceneManager.LoadScene(nameLocation);
        if (SwitchSceneValidation.HasScene(nameSwitchLocation)) SceneManager.LoadScene(nameLocation);
        else Debug.Log(SceneNotFoundException.SceneNotFound(nameLocation));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.OpenSwitchScenePanel(this);
        }
    }

}
