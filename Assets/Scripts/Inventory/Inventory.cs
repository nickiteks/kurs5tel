﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public Database data;

    public Item item;

    public List<ItemInventory> items = new List<ItemInventory>();

    public GameObject gameObjectShow;

    public GameObject InventoryMainObject;

    public int maxCount;

    public Camera cam;
    public EventSystem es;

    public int currentID;
    public ItemInventory currentItem;

    public RectTransform movingObject;
    public Vector3 offset;

    public GameObject backGround;

    public void Start()
    {
        if(items.Count == 0)
        {
            AddGraphics();
        }

        for (int i = 0; i<maxCount;i++)//тест заполнения
        {
            AddItem(i,item, Random.Range(1, 64));
        }
        //AddItem(4, data.items[1], Random.Range(1, 64));
        UpdateInventiory();
    }
    public void Update()
    {
        if(currentID != -1)
        {
            MoveObject();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            backGround.SetActive(!backGround.activeSelf);
            if (backGround.activeSelf)
            {
                UpdateInventiory();
            }
        }
    }
    public void SearchForSameItem(Item item, int count)
    {
        for(int i = 0; i < maxCount; i++)
        {
            if(items[i].id == item.id)
            {
                if(items[0].count < 64)
                {
                    items[i].count += count;
                    if (items[i].count > 64)
                    {
                        count = items[i].count - 64;
                        items[i].count = 32;
                    }
                    else
                    {
                        count = 0;
                        i = maxCount;
                    }
                }
            }
        }
        if (count > 0)
        {
            for(int i =0;i < maxCount; i++)
            {
                if(items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCount;
                }
            }
        }
    }
    public void AddItem(int id,Item item,int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObject.GetComponent<Image>().sprite = item.img;

        if(count > 1 && item.id  != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObject.GetComponent<Image>().sprite = data.items[invItem.id].img;

        if (invItem.count > 1 && invItem.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraphics()
    {   
        for(int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjectShow,InventoryMainObject.transform) as GameObject;

            newItem.name = i.ToString();
            ItemInventory ii = new ItemInventory();
            ii.itemGameObject = newItem;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(ii);
        }
    }

    public void UpdateInventiory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id !=0 )
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = "";
            }
            items[i].itemGameObject.GetComponent<Image>().sprite = data.items[items[i].id].img;
        }
    }

    public void SelectObject()
    {
        if(currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(items[currentID]);
            movingObject.gameObject.SetActive(true);
            movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

            AddItem(currentID, data.items[0],0);
        }
        else
        {
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

            if (currentItem.id != II.id)
            {
                AddInventoryItem(currentID, II);

                AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
            }
            else
            {
                if(II.count + currentItem.count <= 64)
                {
                    II.count += currentItem.count;
                }
                else
                {
                    AddItem(currentID, data.items[II.id], II.count + currentItem.count - 64);
                    II.count = 64;
                }
                if (II.id != 0)
                {
                    II.itemGameObject.GetComponentInChildren<Text>().text = II.count.ToString();
                }
            }

            currentID = -1;

            movingObject.gameObject.SetActive(false);
        }
    }

    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z;
        movingObject.position = cam.ScreenToWorldPoint(pos);
        
    }
    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();

        New.id = old.id;
        New.itemGameObject = old.itemGameObject;
        New.count = old.count;

        return New;
    }
}

[System.Serializable]
public class ItemInventory
{
    public int id;
    public GameObject itemGameObject;

    public int count;
}