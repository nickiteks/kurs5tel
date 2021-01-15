using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private Canvas mapCanvas;
    [SerializeField]
    private Image mapImage;

    private bool isMapOpen = false;

    private void Start()
    {
        Texture2D texture = Resources.Load("MapImage/" + SceneManager.GetActiveScene().name) as Texture2D;
        mapImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }

    private void Update()
    {
        if (!isMapOpen && !playerController.IsInteractionWithWorld && Input.GetKeyDown(InputManager.Instance.map))
        {
            UIManager.Instance.OpenCanvas(mapCanvas);
            playerController.IsInteractionWithWorld = true;
            isMapOpen = true;
        }
        else if (isMapOpen && Input.GetKeyDown(InputManager.Instance.map))
        {
            UIManager.Instance.CloseCanvas(mapCanvas);
            playerController.IsInteractionWithWorld = false;
            isMapOpen = false;
        }
    }
}
