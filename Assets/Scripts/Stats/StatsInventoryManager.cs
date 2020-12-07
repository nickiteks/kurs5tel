using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatsInventoryManager : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private EventSystem es;
    [SerializeField]
    private StatsContainer StatsContainer;
    public void CellScanner()
    {
        int itemIndex = int.Parse(es.currentSelectedGameObject.name);
        ItemInventory item = inventory.items[itemIndex];
        Debug.Log(item);
    }
    private void Start()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        Debug.Log(buttons.Length);
        foreach( Button button in buttons)
        {
            button.onClick.AddListener(delegate { CellScanner(); });
        }
    }
}
