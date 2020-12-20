using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowScript : MonoBehaviour
{
    /// <summary>
    /// Точки за которыми нужно следовать
    /// </summary>
    private Queue<FollowPoint> followPoints = new Queue<FollowPoint>();

    /// <summary>
    /// Ссылка на игрока
    /// </summary>
    [SerializeField]
    [Tooltip("Необязательный параметр. Необходим только, если надо преследовать игрока")]
    private PlayerController playerController;

    /// <summary>
    /// Необходимое текущее направление
    /// </summary>
    public MoveDirection CurrentMoveDirection { get; set; }

    /// <summary>
    /// Проверка паузы у currentFollowPoint
    /// </summary>
    private bool canMove;


    private float currentDistance;
    private float lastDistance;

    private void Start()
    {
        CurrentMoveDirection = MoveDirection.None;
        canMove = true;
    }

    private void Update()
    {
        if (followPoints.Count == 0) return;
        // если есть ссылка на игрока, то синхранизируем остановку персонажа и игрока
        if (playerController) SynchronizationStopingWithPlayer();
        if (!canMove)
        {
            CurrentMoveDirection = MoveDirection.None;
            return;
        }

        
    }

    private void FixedUpdate()
    {
        if (followPoints.Count == 0) return;
        // если мы достигли точки или случайно прошли мимо...
        if ((currentDistance = Vector2.Distance(transform.position, followPoints.Peek().Position)) <= 0.1 || lastDistance > currentDistance)
        {
            // удаляем точку
            followPoints.Dequeue();
            canMove = false;

            CurrentMoveDirection = followPoints.Peek().NextMoveDirection;

            // останавливаемся персонажа на необходимое время
            Invoke(nameof(SetValueCanMoveTrue), followPoints.Peek().DelayTime);

            lastDistance = currentDistance;
        } 
        else
        {
            lastDistance = currentDistance;
        }
    }

    public void AddFollowPoint(FollowPoint followPoint)
    {
        followPoints.Enqueue(followPoint);
        CurrentMoveDirection = followPoints.Peek().NextMoveDirection;
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
        canMove = playerController.CurrentMoveDirection != MoveDirection.None;
    }
}
