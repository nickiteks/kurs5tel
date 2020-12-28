using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollowing : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private new Transform transform;
    private float speed;

    [Space(5)]

    [SerializeField]
    private float minScrollValue = 5;
    [SerializeField]
    private float maxScrollValue = 10;
    [SerializeField]
    private float scale = 1;

    private new Camera camera;

    private void Awake()
    {
        speed = target.GetComponent<MovementScript>().Speed;
        transform = GetComponent<Transform>();
        camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), speed);
    }

    private void Update()
    {
        float scrollValue = Input.mouseScrollDelta.y;
        Debug.Log(scrollValue);
        if (scrollValue > 0 && camera.orthographicSize > minScrollValue)
        {
            camera.orthographicSize -= scale;
        }
        else if (scrollValue < 0 && camera.orthographicSize < maxScrollValue)
        {
            camera.orthographicSize += scale;
        }
    }
}