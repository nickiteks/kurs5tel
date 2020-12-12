using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Скрипт для передвижения объекта
    /// </summary>
    private MovementScript movementScript;
    /// <summary>
    /// Менеджер кнопок
    /// </summary>
    private InputManager inputManager;
    /// <summary>
    /// Очередь нажатых кнопок. Первый элемент - текущий
    /// </summary>
    private List<MoveDirection> queueMovingButton;
    /// <summary>
    /// Компонент отвечающий за анимацию объекта
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Вкл и Выкл режима взаимодействия с объектами в мире
    /// </summary>
    public bool IsInteractionWithWorld 
    {
        get => IsInteractionWithWorld;
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
        }
    }

    private void Awake()
    {
        movementScript = GetComponent<MovementScript>();
        animator = GetComponent<Animator>();

        inputManager = InputManager.Instance;
        queueMovingButton = new List<MoveDirection>() { MoveDirection.None };
    }

    private void Update()
    {
        if (!IsInteractionWithWorld)
        {
            DownUpButtonLogic();
            MovementAnimationLogic();
        }
    }

    private void FixedUpdate()
    {
        MovementLogic();
    }

    /// <summary>
    /// Логика передвижения
    /// </summary>
    private void MovementLogic()
    {
        movementScript.Move(queueMovingButton[0]);
    }

    /// <summary>
    /// Логика нажатия конопок передвижения
    /// </summary>
    private void DownUpButtonLogic()
    {
        if (Input.GetKeyDown(inputManager.moveRight)) queueMovingButton.Insert(0, MoveDirection.Right);
        if (Input.GetKeyDown(inputManager.moveLeft)) queueMovingButton.Insert(0, MoveDirection.Left);
        if (Input.GetKeyDown(inputManager.moveUp)) queueMovingButton.Insert(0, MoveDirection.Up);
        if (Input.GetKeyDown(inputManager.moveDown)) queueMovingButton.Insert(0, MoveDirection.Down);

        if (Input.GetKeyUp(inputManager.moveRight)) queueMovingButton.Remove(MoveDirection.Right);
        if (Input.GetKeyUp(inputManager.moveLeft)) queueMovingButton.Remove(MoveDirection.Left);
        if (Input.GetKeyUp(inputManager.moveUp)) queueMovingButton.Remove(MoveDirection.Up);
        if (Input.GetKeyUp(inputManager.moveDown)) queueMovingButton.Remove(MoveDirection.Down);
    }

    private void MovementAnimationLogic()
    {
        if (animator)
        {
            switch (queueMovingButton[0])
            {
                case MoveDirection.Up:
                    animator.SetBool("BackRun", true);
                    animator.SetBool("SideRun", false);
                    animator.SetBool("FrontRun", false);
                    break;
                case MoveDirection.Down:
                    animator.SetBool("BackRun", false);
                    animator.SetBool("SideRun", false);
                    animator.SetBool("FrontRun", true);
                    break;
                case MoveDirection.Right:
                case MoveDirection.Left:
                    animator.SetBool("BackRun", false);
                    animator.SetBool("SideRun", true);
                    animator.SetBool("FrontRun", false);
                    break;
                case MoveDirection.None:
                    animator.SetBool("BackRun", false);
                    animator.SetBool("SideRun", false);
                    animator.SetBool("FrontRun", false);
                    break;
            }
        }
    }
}
