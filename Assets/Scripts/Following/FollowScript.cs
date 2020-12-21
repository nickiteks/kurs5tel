using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowScript : MonoBehaviour
{
    /// <summary>
    /// Точки за которыми нужно следовать
    /// </summary>
    private readonly Queue<FollowPoint> followPoints = new Queue<FollowPoint>();
    private List<FollowPoint> followPointsList = new List<FollowPoint>();
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
    public MoveDirection CurrentMoveDirection { get; set; }

    /// <summary>
    /// Проверка паузы у currentFollowPoint
    /// </summary>
    private bool canMove;


    private float currentDistance = 1;
    private float lastDistance = 1000;

    private void Start()
    {
        CurrentMoveDirection = MoveDirection.None;
        canMove = true;
    }

    private void FixedUpdate()
    {
        float i = 0;
        if (playerController) SynchronizationStopingWithPlayer();
        if (followPoints.Count == 0 || !canMove) return;


        currentDistance = Vector2.Distance(transform.position, followPoints.Peek().Position);
        Debug.Log($"{transform.position.x}   {transform.position.y}  1");
        // если мы достигли точки или случайно прошли мимо...
        if (currentDistance <= 0.1 || (lastDistance < currentDistance))
        {
            //Debug.Log($"Current Distance - {currentDistance} Last Distance - {lastDistance} Position - {transform.position} | FollowPoint - {followPoints.Peek()}");
            Instantiate(gameObject1, transform.position, Quaternion.identity);
            // удаляем точку
            followPoints.Dequeue();
            followPointsList.RemoveAt(0);
            canMove = false;

            //CurrentMoveDirection = MoveDirection.None;

            // останавливаемся персонажа на необходимое время
            Invoke(nameof(SetValueCanMoveTrue), followPoints.Peek().DelayTime);
            
            lastDistance = Vector2.Distance(transform.position, followPoints.Peek().Position);
            Debug.Log($"{transform.position.x}   {transform.position.y}  2");

            Debug.Log($"Current Distance - {currentDistance} Last Distance - {lastDistance} Position - {transform.position} | FollowPoint - {followPoints.Peek()}");
        } 
        else
        {
            i = lastDistance;
            lastDistance = currentDistance;
        }

        //Debug.Log($"Last Distance - {i} Current Distance - {currentDistance} Position - {transform.position}");
    }

    public void AddFollowPoint(FollowPoint followPoint)
    {
        followPoints.Enqueue(followPoint);
        followPointsList.Add(followPoint);
        
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
            CurrentMoveDirection = MoveDirection.None;
        }
        else
        {
            canMove = true;
            CurrentMoveDirection = followPoints.Peek().NextMoveDirection;
        }
        
    }
}
