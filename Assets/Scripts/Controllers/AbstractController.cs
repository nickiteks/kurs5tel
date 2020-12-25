using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AbstractController : MonoBehaviour
{
    /// <summary>
    /// Скрипт предназначенный для управления передвижением
    /// </summary>
    protected MovementScript movementScript;

    /// <summary>
    /// Компонент отвечающий за анимацию
    /// </summary>
    protected Animator animator;

    /// <summary>
    /// Логика передвижения
    /// </summary>
    protected abstract void MovementLogic();

    /// <summary>
    /// Логика отрисовки анимации передвижения
    /// </summary>
    protected virtual void MovementAnimationLogic(MoveDirection direction)
    {
        if (animator)
        {
            switch (direction)
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
