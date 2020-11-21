using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MovementScript movementScript;
    private float axisHorizontal;
    private float axisVertical;

    private void Awake()
    {
        movementScript = GetComponent<MovementScript>();
    }

    private void Update()
    {
        axisHorizontal = Input.GetAxis("Horizontal");
        axisVertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        MovementLogic();
    }

    private void MovementLogic()
    {
        if (movementScript)
        {
            movementScript.Move(axisVertical, axisHorizontal);
        }
    }
}
