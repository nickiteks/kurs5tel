using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private int speed;

    private void Awake()
    {
        speed = target.GetComponent<MovementScript>().Speed;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -10), speed);
    }
}
