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

    private void FixedUpdate()
    {
        if (followScript.CurrentPoint != null)
        {
            MovementLogic();
            MovementAnimationLogic(followScript.CurrentPoint.NextMoveDirection);
        }
        else
        {
            MovementAnimationLogic(MoveDirection.None);
        }
    }

    protected override void MovementLogic()
    {
        movementScript.Move(followScript.CurrentPoint.Position);
    }
}
