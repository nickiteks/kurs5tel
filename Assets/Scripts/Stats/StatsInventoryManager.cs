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
    private BaseStatsScript StatsContainer;
    public Database database = new Database();
    public MovingObgectManager movingObgect;
    public void CellScanner()
    {
        int itemIndex = int.Parse(es.currentSelectedGameObject.name);
        if (movingObgect.ItemInventory == null)
        {
            StatsContainer.Armor += database.items[inventory.items[itemIndex].id].armor;
        }
        else
        {
            StatsContainer.Armor -= database.items[movingObgect.ItemInventory.id].armor;
        }
        if (StatsContainer.Armor < 0)
        {
            StatsContainer.Armor = 0;
        }
    }
    public void InventiryScanner()
    {
        StatsContainer.Armor = 0;
        foreach (var item in inventory.items)
        {
            StatsContainer.Armor += database.items[item.id].armor;
        }
    }
    private void Start()
    {
        InventiryScanner();
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(delegate { CellScanner(); });
        }
    }
}
