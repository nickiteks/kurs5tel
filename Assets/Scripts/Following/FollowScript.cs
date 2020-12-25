using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowScript : MonoBehaviour
{
    /// <summary>
    /// Точки за которыми нужно следовать
    /// </summary>
    public List<FollowPoint> followPoints = new List<FollowPoint>();
    /// <summary>
    /// Ссылка на игрока
    /// </summary>
    [SerializeField]
    [Tooltip("Необязательный параметр. Необходим только, если надо преследовать игрока")]
    private PlayerController playerController;

    /// <summary>
    /// Необходимое текущее направление
    /// </summary>
    public FollowPoint CurrentPoint { get; set; }

    [SerializeField]
    private float distanceBtwPlayer;
    public float DistanceBtwPlayer { get { return distanceBtwPlayer; } }
    /// <summary>
    /// Проверка паузы у currentFollowPoint
    /// </summary>
    private bool canMove = false;

    private float distance;

    private void Start()
    {
        //InvokeRepeating(nameof(ControlDistance), 1, 2);
    }

    private void FixedUpdate()
    {
        if (followPoints.Count == 0) return;

        if (playerController) FollowPlayer();
        else FollowPath();
    }

    private void FollowPlayer()
    {
        if (!StopingWithPlayer()) return;
        ControlDistance();

        if (canMove && Vector2.Distance(transform.position, followPoints[0].Position) <= 0.1)
        {
            followPoints.RemoveAt(0);
            CurrentPoint = followPoints[0];
        }
    }

    private void FollowPath()
    {
        if(canMove && Vector2.Distance(transform.position, followPoints[0].Position) <= 0.1)
        {
            transform.position = followPoints[0].Position;

            if (followPoints[0].DelayTime != 0)
            {
                canMove = false;
                Invoke(nameof(SetValueCanMoveTrue), followPoints[0].DelayTime);
            }

            FollowPoint point = followPoints[0];
            followPoints.RemoveAt(0);
            followPoints.Add(point);

            CurrentPoint = followPoints[0];
        }
    }

    public void AddFollowPoint(FollowPoint followPoint)
    {
        followPoints.Add(followPoint);
    }

    /// <summary>
    /// Присвоить значение true переменной canMove
    /// </summary>
    private void SetValueCanMoveTrue()
    {
        canMove = true;
    }

    /// <summary>
    /// Синхронизация остановки с игроком
    /// </summary>
    private bool StopingWithPlayer()
    {
        if (playerController.CurrentMoveDirection == MoveDirection.None || followPoints.Count == 0)
        {
            canMove = false;
            CurrentPoint = null;
            return false;
        }
        else
        {
            canMove = true;
            CurrentPoint = followPoints[0];
            return true;
        }
    }

    private void ControlDistance()
    {
        if (!canMove) return;

        distance = Vector2.Distance(transform.position, followPoints[0].Position);
        for (int i = 0; i < followPoints.Count - 2; i++)
        {
            distance += Vector2.Distance(followPoints[i].Position, followPoints[i + 1].Position);
        }
        distance += Vector2.Distance(followPoints[followPoints.Count - 1].Position, playerController.transform.position);

        if (distance > distanceBtwPlayer + 0.2)
        {
            transform.position = followPoints[0].Position;
        } 
        else if (distance < distanceBtwPlayer - 0.2)
        {
            canMove = false;
            CurrentPoint = null;
        }
    }
}
