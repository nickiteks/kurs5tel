using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotController : AbstractController
{
    protected FollowScript followScript;

    private void Awake()
    {
        followScript = GetComponent<FollowScript>();
        movementScript = GetComponent<MovementScript>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MovementAnimationLogic(followScript.CurrentMoveDirection);
    }

    private void FixedUpdate()
    {
        MovementLogic();
    }

    protected override void MovementLogic()
    {
        movementScript.Move(followScript.CurrentMoveDirection);
    }
}
