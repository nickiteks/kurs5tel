using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Registration : MonoBehaviour
{
    private readonly string APINameFunction = "Register";
    private readonly string APINameController = "Client";

    public IEnumerator Register()
    {
        string url = APIConfig.url();
        using (WWW www = new WWW(url))
        {
            yield return www;
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = www.texture;
        }
    }
}
