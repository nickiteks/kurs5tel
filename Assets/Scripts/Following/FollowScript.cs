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
    public GameObject gameObject1;
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

    /// <summary>
    /// Проверка паузы у currentFollowPoint
    /// </summary>
    private bool canMove = false;

    private void FixedUpdate()
    {
        if (followPoints.Count == 0) return;

        if (playerController) FollowPlayer();
        else FollowPath();
    }

    private void FollowPlayer()
    {
        SynchronizationStopingWithPlayer();
        if (canMove && Vector2.Distance(transform.position, followPoints[0].Position) == 0)
        {
            //transform.position = followPoints[0].Position;
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
    private void SynchronizationStopingWithPlayer()
    {
        if (playerController.CurrentMoveDirection == MoveDirection.None || followPoints.Count == 0)
        {
            canMove = false;
            CurrentPoint = null;
        }
        else
        {
            canMove = true;
            CurrentPoint = followPoints[0];
        }
    }
}
