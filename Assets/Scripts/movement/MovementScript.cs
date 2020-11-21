using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementScript : MonoBehaviour
{
    [SerializeField]
    private int speed = 1;
    public int Speed { get { return speed; } }

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(float axisVertical, float axisHorizontal)
    {
        rb.velocity = new Vector2(axisHorizontal, axisVertical) * speed;

        if (axisHorizontal < 0) transform.rotation = Quaternion.Euler(new Vector2(0, 180));
        else transform.rotation = Quaternion.Euler(new Vector2(0, 0));
    }
}
