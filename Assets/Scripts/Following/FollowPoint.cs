using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
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
    public int index;

    public FollowPoint(int delayTime, Vector2 position, MoveDirection direction, int index)
    {
        NextMoveDirection = direction;
        DelayTime = delayTime;
        Position = position;
        this.index = index;
    }

    public override string ToString()
    {
        return $"NextMoveDirection - {NextMoveDirection} Position - {Position} Index - {index}";
    }
}
