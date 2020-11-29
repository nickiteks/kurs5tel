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

    private void Awake()
    {
        movementScript = GetComponent<MovementScript>();
        inputManager = InputManager.Instance;
        queueMovingButton = new List<MoveDirection>() { MoveDirection.None };
    }

    private void Update()
    {
        DownUpButtonLogic();
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
}
