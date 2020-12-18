using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    protected abstract void MovementAnimationLogic();
}
