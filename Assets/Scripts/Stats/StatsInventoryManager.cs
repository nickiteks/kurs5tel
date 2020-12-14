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
    public DatabaseInventory database = new DatabaseInventory();
    public MovingObgectManager movingObgect;
    public void CellScanner()
    {
        int itemIndex = int.Parse(es.currentSelectedGameObject.name);
        if (movingObgect.ItemInventory == null)
        {
            StatsContainer.Armor += database.items[inventory.items[itemIndex].id].armor;
            StatsContainer.Damage += database.items[inventory.items[itemIndex].id].damage;
            StatsContainer.ManaMax += database.items[inventory.items[itemIndex].id].mana;
        }
        else
        {
            StatsContainer.Armor -= database.items[movingObgect.ItemInventory.id].armor;
            StatsContainer.Damage -= database.items[movingObgect.ItemInventory.id].damage;
            StatsContainer.ManaMax -= database.items[movingObgect.ItemInventory.id].mana;

        }
        if (StatsContainer.Armor < 0)
        {
            StatsContainer.Armor = 0;
        }
        if (StatsContainer.Damage < 0)
        {
            StatsContainer.Damage = 0;
        }
        if (StatsContainer.ManaMax < 0)
        {
            StatsContainer.Damage = 0;
        }
    }
    public void InventiryScanner()
    {
        //StatsContainer.Armor = 0;
        //StatsContainer.Damage = 0;
        foreach (var item in inventory.items)
        {
            StatsContainer.Armor += database.items[item.id].armor;
            StatsContainer.Damage += database.items[item.id].damage;
            StatsContainer.ManaMax += database.items[item.id].mana;
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
