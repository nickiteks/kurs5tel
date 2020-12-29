using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestInventory : Inventory
{
    void Awake()
    {
        logger = new Logger("InventoryLog");
        movingObgectManager = MovingObgectManager.Instance;
        movingObgectManager.ItemInventory = null;
        if (items.Count == 0)
        {
            AddGraphics();
        }

        for (int i = 0; i < maxCount; i++)//тест заполнения
        {
            AddItem(i, data.items[Random.Range(0, 6)], Random.Range(1, cellSize));
        }
        es = FindObjectOfType<EventSystem>();
        UpdateInventiory();
    }
    void Update()
    {
        base.Update();
    }
}
