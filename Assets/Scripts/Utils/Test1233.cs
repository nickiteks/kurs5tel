using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1233 : MonoBehaviour
{
    public GameObject plaer;

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(Vector2.Distance(plaer.transform.position, transform.position));
    }
}
