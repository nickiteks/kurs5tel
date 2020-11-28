using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public Database data;

    public List<ItemInventory> items = new List<ItemInventory>();
    public List<ItemInventory> WarriorItems = new List<ItemInventory>();
    public List<ItemInventory> MageItems = new List<ItemInventory>();
    public List<ItemInventory> ThifItems = new List<ItemInventory>();

    public GameObject gameObjectShow;

    public GameObject InventoryMainObject;
    public GameObject InventoryWarriorObejct;
    public GameObject InventoryMageObejct;
    public GameObject InventoryThiefObject;

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
        if (items.Count == 0)
        {
            AddGraphics();
            AddWarriorGraphics();
            AddMageGraphics();
            AddThiefGraphics();
        }

        for (int i = 0; i < 6; i++)//тест заполнения
        {
            AddItemWarrior(i, data.items[0], Random.Range(1, 64));
            AddItemThief(i, data.items[0], Random.Range(1, 64));
            AddItemMage(i, data.items[0], Random.Range(1, 64));
        }

        for (int i = 0; i < maxCount; i++)//тест заполнения
        {
            AddItem(i, data.items[1], Random.Range(1, 64));
        }

        UpdateInventiory();
    }
    public void Update()
    {
        if (currentID != -1)
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
        if (!IsMouseOverUI() && Input.GetKeyDown(KeyCode.Mouse0))
        {
            DeleteItem();
            movingObject.gameObject.SetActive(false);
        }
    }
    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObject.GetComponent<Image>().sprite = item.img;

        if (count > 1 && item.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddItemWarrior(int id, Item item, int count)
    {
        WarriorItems[id].id = item.id;
        WarriorItems[id].count = count;
        WarriorItems[id].itemGameObject.GetComponent<Image>().sprite = data.items[0].img;

        if (count > 1 && item.id != 0)
        {
            WarriorItems[id].itemGameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            WarriorItems[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }
    public void AddItemMage(int id, Item item, int count)
    {
        MageItems[id].id = item.id;
        MageItems[id].count = count;
        MageItems[id].itemGameObject.GetComponent<Image>().sprite = data.items[0].img;

        if (count > 1 && item.id != 0)
        {
            MageItems[id].itemGameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            MageItems[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }
    public void AddItemThief(int id, Item item, int count)
    {
        ThifItems[id].id = item.id;
        ThifItems[id].count = count;
        ThifItems[id].itemGameObject.GetComponent<Image>().sprite = data.items[0].img;

        if (count > 1 && item.id != 0)
        {
            ThifItems[id].itemGameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            ThifItems[id].itemGameObject.GetComponentInChildren<Text>().text = "";
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
    public void AddInventoryItemWarrior(int id, ItemInventory invItem)
    {
        WarriorItems[id].id = invItem.id;
        WarriorItems[id].count = invItem.count;
        WarriorItems[id].itemGameObject.GetComponent<Image>().sprite = data.items[invItem.id].img;

        if (invItem.count > 1 && invItem.id != 0)
        {
            WarriorItems[id].itemGameObject.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            WarriorItems[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }
    public void AddInventoryItemMage(int id, ItemInventory invItem)
    {
        MageItems[id].id = invItem.id;
        MageItems[id].count = invItem.count;
        MageItems[id].itemGameObject.GetComponent<Image>().sprite = data.items[invItem.id].img;

        if (invItem.count > 1 && invItem.id != 0)
        {
            MageItems[id].itemGameObject.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            MageItems[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }
    public void AddInventoryItemThief(int id, ItemInventory invItem)
    {
        ThifItems[id].id = invItem.id;
        ThifItems[id].count = invItem.count;
        ThifItems[id].itemGameObject.GetComponent<Image>().sprite = data.items[invItem.id].img;

        if (invItem.count > 1 && invItem.id != 0)
        {
            ThifItems[id].itemGameObject.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            ThifItems[id].itemGameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjectShow, InventoryMainObject.transform) as GameObject;


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

    public void AddWarriorGraphics()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject WarriorItem = Instantiate(gameObjectShow, InventoryWarriorObejct.transform) as GameObject;

            WarriorItem.name = i.ToString();
            ItemInventory iiW = new ItemInventory();
            iiW.itemGameObject = WarriorItem;

            RectTransform rtW = WarriorItem.GetComponent<RectTransform>();
            rtW.localPosition = new Vector3(0, 0, 0);
            rtW.localScale = new Vector3(1, 1, 1);
            WarriorItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = WarriorItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObjectWarriror(); });

            WarriorItems.Add(iiW);
        }
    }

    public void AddMageGraphics()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject MageItem = Instantiate(gameObjectShow, InventoryMageObejct.transform) as GameObject;

            MageItem.name = i.ToString();
            ItemInventory iiM = new ItemInventory();
            iiM.itemGameObject = MageItem;

            RectTransform rtW = MageItem.GetComponent<RectTransform>();
            rtW.localPosition = new Vector3(0, 0, 0);
            rtW.localScale = new Vector3(1, 1, 1);
            MageItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = MageItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObjectMage(); });

            MageItems.Add(iiM);
        }
    }
    public void AddThiefGraphics()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject ThiefItem = Instantiate(gameObjectShow, InventoryThiefObject.transform) as GameObject;

            ThiefItem.name = i.ToString();
            ItemInventory iiT = new ItemInventory();
            iiT.itemGameObject = ThiefItem;

            RectTransform rtW = ThiefItem.GetComponent<RectTransform>();
            rtW.localPosition = new Vector3(0, 0, 0);
            rtW.localScale = new Vector3(1, 1, 1);
            ThiefItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = ThiefItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObjectThief(); });

            ThifItems.Add(iiT);
        }
    }

    public void UpdateInventiory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id != 0)
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObject.GetComponentInChildren<Text>().text = "";
            }
            items[i].itemGameObject.GetComponent<Image>().sprite = data.items[items[i].id].img;
        }
        for (int i = 0; i < 6; i++)
        {
            if (WarriorItems[i].id != 0)
            {
                WarriorItems[i].itemGameObject.GetComponentInChildren<Text>().text = WarriorItems[i].count.ToString();
            }
            else
            {
                WarriorItems[i].itemGameObject.GetComponentInChildren<Text>().text = "";
            }
            WarriorItems[i].itemGameObject.GetComponent<Image>().sprite = data.items[WarriorItems[i].id].img;
        }
        for (int i = 0; i < 6; i++)
        {
            if (MageItems[i].id != 0)
            {
                MageItems[i].itemGameObject.GetComponentInChildren<Text>().text = MageItems[i].count.ToString();
            }
            else
            {
                MageItems[i].itemGameObject.GetComponentInChildren<Text>().text = "";
            }
            MageItems[i].itemGameObject.GetComponent<Image>().sprite = data.items[MageItems[i].id].img;
        }
        for (int i = 0; i < 6; i++)
        {
            if (ThifItems[i].id != 0)
            {
                ThifItems[i].itemGameObject.GetComponentInChildren<Text>().text = ThifItems[i].count.ToString();
            }
            else
            {
                ThifItems[i].itemGameObject.GetComponentInChildren<Text>().text = "";
            }
            ThifItems[i].itemGameObject.GetComponent<Image>().sprite = data.items[ThifItems[i].id].img;
        }
    }

    public void SelectObjectWarriror()
    {
        if (currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(WarriorItems[currentID]);
            if (currentItem.id != 0)
            {
                movingObject.gameObject.SetActive(true);
                movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

                AddItemWarrior(currentID, data.items[0], 0);
            }
        }
        else
        {
            if(currentItem.id != 0)
            {
                while (true)
                {
                    if (currentID > 5)
                    {
                        currentID = currentID - 6;
                    }
                    else
                    {
                        break;
                    }
                }
                ItemInventory II = WarriorItems[int.Parse(es.currentSelectedGameObject.name)];

                if (currentItem.id != II.id)
                {
                    AddInventoryItemWarrior(int.Parse(es.currentSelectedGameObject.name), currentItem);
                }
                else
                {
                    if (II.count + currentItem.count <= 64)
                    {
                        II.count += currentItem.count;
                    }
                    else
                    {
                        AddItemWarrior(currentID, data.items[II.id], II.count + currentItem.count - 64);
                        II.count = 64;
                    }
                    if (II.id != 0)
                    {
                        II.itemGameObject.GetComponentInChildren<Text>().text = II.count.ToString();
                    }
                }
            }

            currentID = -1;

            movingObject.gameObject.SetActive(false);
        }
    }
    public void SelectObjectMage()
    {
        if (currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(MageItems[currentID]);
            if (currentItem.id != 0)
            {
                movingObject.gameObject.SetActive(true);
                movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

                AddItemMage(currentID, data.items[0], 0);
            }
        }
        else
        {
            if (currentItem.id != 0)
            {
                while (true)
                {
                    if (currentID > 5)
                    {
                        currentID = currentID - 6;
                    }
                    else
                    {
                        break;
                    }
                }
                ItemInventory II = MageItems[int.Parse(es.currentSelectedGameObject.name)];

                if (currentItem.id != II.id)
                {
                    AddInventoryItemMage(int.Parse(es.currentSelectedGameObject.name), currentItem);
                }
                else
                {
                    if (II.count + currentItem.count <= 64)
                    {
                        II.count += currentItem.count;
                    }
                    else
                    {
                        AddItemMage(currentID, data.items[II.id], II.count + currentItem.count - 64);
                        II.count = 64;
                    }
                    if (II.id != 0)
                    {
                        II.itemGameObject.GetComponentInChildren<Text>().text = II.count.ToString();
                    }
                }
            }

            currentID = -1;

            movingObject.gameObject.SetActive(false);
        }
    }
    public void SelectObjectThief()
    {
        if (currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(ThifItems[currentID]);
            if (currentItem.id != 0)
            {
                movingObject.gameObject.SetActive(true);
                movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

                AddItemThief(currentID, data.items[0], 0);
            }
        }
        else
        {
            if (currentItem.id != 0)
            {
                while (true)
                {
                    if (currentID > 5)
                    {
                        currentID = currentID - 6;
                    }
                    else
                    {
                        break;
                    }
                }
                ItemInventory II = ThifItems[int.Parse(es.currentSelectedGameObject.name)];

                if (currentItem.id != II.id)
                {
                    AddInventoryItemThief(int.Parse(es.currentSelectedGameObject.name), currentItem);
                }
                else
                {
                    if (II.count + currentItem.count <= 64)
                    {
                        II.count += currentItem.count;
                    }
                    else
                    {
                        AddItemThief(currentID, data.items[II.id], II.count + currentItem.count - 64);
                        II.count = 64;
                    }
                    if (II.id != 0)
                    {
                        II.itemGameObject.GetComponentInChildren<Text>().text = II.count.ToString();
                    }
                }
            }

            currentID = -1;

            movingObject.gameObject.SetActive(false);
        }
    }

    public void SelectObject()
    {
        if (currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(items[currentID]);
            if(currentItem.id != 0)
            {
                movingObject.gameObject.SetActive(true);
                movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

                AddItem(currentID, data.items[0], 0);
            }
        }
        else
        {
            if (currentItem.id != 0)
            {
                ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

                if (currentItem.id != II.id)
                {
                    AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
                }
                else
                {
                    if (II.count + currentItem.count <= 64)
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
            }
            currentID = -1;

            movingObject.gameObject.SetActive(false);
        }
    }

    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z;
        pos.z = InventoryWarriorObejct.GetComponent<RectTransform>().position.z;
        pos.z = InventoryMageObejct.GetComponent<RectTransform>().position.z;
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
    public void DeleteItem()
    {
        int index = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == 0)
            {
                index = i;
            }
        }
        currentItem = CopyInventoryItem(items[index]);
        currentID = -1;
    }
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}

[System.Serializable]
public class ItemInventory
{
    public int id;
    public GameObject itemGameObject;
    public int count;
}