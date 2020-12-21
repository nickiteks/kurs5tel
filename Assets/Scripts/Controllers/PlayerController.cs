using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : AbstractController
{
    /// <summary>
    /// Менеджер кнопок
    /// </summary>
    private InputManager inputManager;
    /// <summary>
    /// Очередь нажатых кнопок. Первый элемент - текущий
    /// </summary>
    private List<MoveDirection> queueMovingButton;

    /// <summary>
    /// Текущее направление игрока
    /// </summary>
    public MoveDirection CurrentMoveDirection { get { return queueMovingButton[0]; } }

    /// <summary>
    /// Задаёт направление для преследователей
    /// </summary>
    private MainPlayerFollowScript mainPlayerFollowScript;

    /// <summary>
    /// Вкл и Выкл режима взаимодействия с объектами в мире
    /// </summary>
    private bool isInteractionWithWorld;
    public bool IsInteractionWithWorld 
    {
        get { return isInteractionWithWorld; }
        set
        {
            if (value)
            {
                movementScript.enabled = false;
                animator.enabled = false;
            } 
            else
            {
                movementScript.enabled = true;
                animator.enabled = true;
            }

            isInteractionWithWorld = value;
        }
    }

    private Vector2 distance = new Vector2(0, 0);
    int index = 0;
    private void Awake()
    {
        movementScript = GetComponent<MovementScript>();
        animator = GetComponent<Animator>();
        mainPlayerFollowScript = GetComponent<MainPlayerFollowScript>();

        inputManager = InputManager.Instance;
        queueMovingButton = new List<MoveDirection>() { MoveDirection.None };
    }

    private void Update()
    {
        if (!IsInteractionWithWorld)
        {
            DownUpButtonLogic();
            MovementAnimationLogic(queueMovingButton[0]);
        }
    }

    private void FixedUpdate()
    {
        MovementLogic();
        ControlSwitchMoveDirection();
    }

    protected override void MovementLogic()
    {
        movementScript.Move(queueMovingButton[0]);
    }

    /// <summary>
    /// Логика нажатия конопок передвижения
    /// </summary>
    private void DownUpButtonLogic()
    {
        if (Input.GetKeyDown(inputManager.moveRight))
        {
            queueMovingButton.Insert(0, MoveDirection.Right);
            mainPlayerFollowScript.CreateFollowPoint(new FollowPoint(0, new Vector2(transform.position.x, transform.position.y), queueMovingButton[0], index++));
            distance = transform.position;
        }
        if (Input.GetKeyDown(inputManager.moveLeft))
        {
            queueMovingButton.Insert(0, MoveDirection.Left);
            mainPlayerFollowScript.CreateFollowPoint(new FollowPoint(0, new Vector2(transform.position.x, transform.position.y), queueMovingButton[0], index++));
            distance = transform.position;
        }
        if (Input.GetKeyDown(inputManager.moveUp)) {
            queueMovingButton.Insert(0, MoveDirection.Up);
            mainPlayerFollowScript.CreateFollowPoint(new FollowPoint(0, new Vector2(transform.position.x, transform.position.y), queueMovingButton[0], index++));
            distance = transform.position;
        }
        if (Input.GetKeyDown(inputManager.moveDown)) {
            queueMovingButton.Insert(0, MoveDirection.Down);
            mainPlayerFollowScript.CreateFollowPoint(new FollowPoint(0, new Vector2(transform.position.x, transform.position.y), queueMovingButton[0], index++));
            distance = transform.position;
        }

        if (Input.GetKeyUp(inputManager.moveRight)) queueMovingButton.Remove(MoveDirection.Right);
        if (Input.GetKeyUp(inputManager.moveLeft)) queueMovingButton.Remove(MoveDirection.Left);
        if (Input.GetKeyUp(inputManager.moveUp)) queueMovingButton.Remove(MoveDirection.Up);
        if (Input.GetKeyUp(inputManager.moveDown)) queueMovingButton.Remove(MoveDirection.Down);
    }

    /// <summary>
    /// Отлавливание смены направления
    /// </summary>
    private void ControlSwitchMoveDirection()
    {
        if (Vector2.Distance(transform.position, distance) >= 1)
        {
            mainPlayerFollowScript.CreateFollowPoint(new FollowPoint(0, new Vector2(transform.position.x, transform.position.y), queueMovingButton[0], index++));
            distance = transform.position;
        }
            
    }
}
