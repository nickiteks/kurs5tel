using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Rigidbody2D))]
public class MovementScript : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } }

    [SerializeField]
    private Rigidbody2D rb;

    private Vector2 horizontalDirection = new Vector2(1, 0);
    private Vector2 verticalDirection = new Vector2(0, 1);

    private new Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    /// <summary>
    /// Идти по заданному напрвлению
    /// </summary>
    /// <param name="direction"></param>
    public void Move(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.None:
                rb.velocity = new Vector2(0, 0);
                break;
            case MoveDirection.Right:
                transform.rotation = Quaternion.Euler(new Vector2(0, 0));
                rb.velocity = horizontalDirection * speed;
                break;
            case MoveDirection.Left:
                transform.rotation = Quaternion.Euler(new Vector2(0, 180));
                rb.velocity = -horizontalDirection * speed;
                break;
            case MoveDirection.Up:
                transform.rotation = Quaternion.Euler(new Vector2(0, 0));
                rb.velocity = verticalDirection * speed;
                break;
            case MoveDirection.Down:
                transform.rotation = Quaternion.Euler(new Vector2(0, 0));
                rb.velocity = -verticalDirection * speed;
                break;
        }
    }

    /// <summary>
    /// Идти к точке
    /// </summary>
    /// <param name="position"></param>
    public void Move(Vector2 position)
    {
        transform.position = Vector2.MoveTowards(transform.position, position, speed);
    }
}
