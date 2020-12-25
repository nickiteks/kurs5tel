using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SwitchSceneValidation 
{
    public static bool HasScene(string nameScene)
    {
        return SceneManager.GetSceneByName(nameScene).name == nameScene;
    }
}
