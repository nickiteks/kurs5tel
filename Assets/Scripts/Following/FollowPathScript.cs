using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPathScript : MonoBehaviour, IFollow
{
    /// <summary>
    /// Точки за которыми нужно следовать
    /// </summary>
    public List<FollowWorldPoint> followPoints = new List<FollowWorldPoint>();

    /// <summary>
    /// Проверка паузы у currentFollowPoint
    /// </summary>
    private bool canMove = true;

    public FollowPoint CurrentPoint { get; set; }

    private void Start()
    {
        CurrentPoint = new FollowPoint(followPoints[0].DelayTime, followPoints[0].transform.position, followPoints[0].NextMoveDirection);
    }

    private void FixedUpdate()
    {
        FollowPath();
    }
    private void FollowPath()
    {
        if (canMove && Vector2.Distance(transform.position, followPoints[0].transform.position) <= 0.1)
        {
            transform.position = followPoints[0].transform.position;

            if (followPoints[0].DelayTime != 0)
            {
                canMove = false;
                CurrentPoint = null;
                Invoke(nameof(SetValueCanMoveTrue), followPoints[0].DelayTime);
            } 
            else
            {
                SwitchPoint();
                CurrentPoint = new FollowPoint(followPoints[0].DelayTime, followPoints[0].transform.position, followPoints[0].NextMoveDirection);
            }
        }

    }

    private void SetValueCanMoveTrue()
    {
        canMove = true;
        SwitchPoint();
        CurrentPoint = new FollowPoint(followPoints[0].DelayTime, followPoints[0].transform.position, followPoints[0].NextMoveDirection);
    }

    private void SwitchPoint()
    {
        FollowWorldPoint point = followPoints[0];
        followPoints.RemoveAt(0);
        followPoints.Add(point);
    }
}
