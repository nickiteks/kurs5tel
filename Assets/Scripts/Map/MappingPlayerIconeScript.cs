using UnityEngine;
using UnityEngine.Tilemaps;

public class MappingPlayerIconeScript : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    [Tooltip("Границы в которых может передвигаться игрок")]
    private Tilemap mapBounds;

    [SerializeField]
    [Tooltip("Икона игрока на карте")]
    private RectTransform iconeMapPlayer;

    [SerializeField]
    private RectTransform mapImageRectTransform;

    [Tooltip("Центр отсчета границы карты")]
    Vector2 mapPosition;
    [Tooltip("Размер карты")]
    Vector2 mapSize;

    Vector3[] cornersPanel = new Vector3[4];

    private void Start()
    {
        mapPosition = mapBounds.localBounds.center;
        mapSize = mapBounds.localBounds.extents;

        mapImageRectTransform.GetLocalCorners(cornersPanel);
        OrientPlayerIcone();
    }

    private void OnEnable()
    {
        OrientPlayerIcone();
    }

    private void OrientPlayerIcone()
    {
        Vector2 playerPosInMap = new Vector2(player.position.x - mapPosition.x, player.position.y - mapPosition.y);

        // Вычиляем соотшение игрока к большой карте
        float width = playerPosInMap.x / mapSize.x;
        float height = playerPosInMap.y / mapSize.y;

        // Вычисляем координаты игрока к картике карты
        width *= Mathf.Abs(cornersPanel[0].x);
        height *= Mathf.Abs(cornersPanel[0].y);

        iconeMapPlayer.position = new Vector2(width + Mathf.Abs(cornersPanel[0].x) + 50, height + Mathf.Abs(cornersPanel[0].y) + 50);
    }
}
