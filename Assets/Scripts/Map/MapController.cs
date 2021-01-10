using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private RectTransform mapPanel;

    [SerializeField]
    private Image mapImage;

    private bool isMapOpen = false;

    private void Awake()
    {
        mapImage.sprite = Resources.Load("MapImage/" + SceneManager.GetActiveScene().name + "Map") as Sprite;
    }

    private void Update()
    {
        if (!isMapOpen && !playerController.IsInteractionWithWorld && Input.GetKeyDown(InputManager.Instance.map))
        {
            UIManager.Instance.OpenPanel(mapPanel);
            playerController.IsInteractionWithWorld = true;
            isMapOpen = true;
        }
        else if (isMapOpen && Input.GetKeyDown(InputManager.Instance.map))
        {
            UIManager.Instance.ClosePanel(mapPanel);
            playerController.IsInteractionWithWorld = false;
            isMapOpen = false;
        }
    }
}
