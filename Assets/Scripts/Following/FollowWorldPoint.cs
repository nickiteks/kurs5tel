using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowWorldPoint : MonoBehaviour
{
    /// <summary>
    /// Направление движение, которое нужно сохранять после достижения этой точки
    /// </summary>
    public MoveDirection NextMoveDirection;

    /// <summary>
    /// Остановка на месте перед движением
    /// </summary>
    public int DelayTime;
}
