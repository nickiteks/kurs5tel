using UnityEngine;
using UnityEngine.EventSystems;

public class FollowPoint
{
    /// <summary>
    /// Направление движение, которое нужно сохранять после достижения этой точки
    /// </summary>
    public MoveDirection NextMoveDirection { get; private set; }

    /// <summary>
    /// Остановка на месте перед движением
    /// </summary>
    public int DelayTime { get; private set; }

    /// <summary>
    /// Точка смены направления, которую надо достич
    /// </summary>
    public Vector2 Position { get; private set; }

    public FollowPoint(int delayTime, Vector2 position, MoveDirection direction)
    {
        NextMoveDirection = direction;
        DelayTime = delayTime;
        Position = position;
    }
}
