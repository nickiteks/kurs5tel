using UnityEngine;
using UnityEngine.Tilemaps;

public class MappingPlayerIconeScript : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    [Tooltip("Границы в которых может передвигаться игрок")]
    private Tilemap mapBounds;

    [Tooltip("Икона игрока на карте")]
    private RectTransform iconeMapPlayer;

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
}
