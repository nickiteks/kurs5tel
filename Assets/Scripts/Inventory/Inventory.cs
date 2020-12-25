using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Inventory : MonoBehaviour
{
    public DatabaseInventory data;

    public List<ItemInventory> items = new List<ItemInventory>();

    [SerializeField]
    private GameObject gameObjectShow;

    [SerializeField]
    private GameObject InventoryMainObject;

    [SerializeField]
    private int maxCount;

    private EventSystem es;

    private int currentID = -1;

    [SerializeField]
    private RectTransform movingObject;
    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private int cellSize;

    private MovingObgectManager movingObgectManager;

    ExceptionsInventory ExceptionsInventory;
    ValidationInventory validationInventory;
    private Logger logger;
    public void Awake()
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
            AddItem(i, data.items[Random.Range(0,6)], Random.Range(1, cellSize));
        }
        es = FindObjectOfType<EventSystem>();
        UpdateInventiory();
    }
    public void Update()
    {
        if (currentID != -1)
        {
            MoveObject();
        }
        if (!IsMouseOverUI() && Input.GetKeyDown(KeyCode.Mouse0))
        {
            DeleteItem();
            movingObject.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Добавление из базы предметов в инвентарь
    /// </summary>
    /// <param name="id"></param>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObject.GetComponent<Image>().sprite = item.img;

        if (count > 1 && item.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<TMP_Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<TMP_Text>().text = "";
        }
        logger.Log("Добавлен предмет " + item.id);
    }
    /// <summary>
    /// добавление уже существующего ItemInventory  в инвентарь
    /// </summary>
    /// <param name="id"></param>
    /// <param name="invItem"></param>
    private void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObject.GetComponent<Image>().sprite = data.items[invItem.id].img;

        if (invItem.count > 1 && invItem.id != 0)
        {
            items[id].itemGameObject.GetComponentInChildren<TMP_Text>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemGameObject.GetComponentInChildren<TMP_Text>().text = "";
        }
    }
    /// <summary>
    /// графика инвентаря
    /// </summary>
    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjectShow, InventoryMainObject.transform) as GameObject;


            newItem.name = i.ToString();
            ItemInventory itemInventory = new ItemInventory
            {
                itemGameObject = newItem
            };

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(itemInventory);
        }
    }

    /// <summary>
    /// обновление данных
    /// </summary>
    private void UpdateInventiory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id != 0)
            {
                items[i].itemGameObject.GetComponentInChildren<TMP_Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObject.GetComponentInChildren<TMP_Text>().text = "";
            }
            items[i].itemGameObject.GetComponent<Image>().sprite = data.items[items[i].id].img;
        }
        if(items.Count > maxCount)
        {
            ExceptionsInventory.OverFlowException();
        }
    }
    /// <summary>
    /// выбор обьекта в инвентаре
    /// </summary>
    private void SelectObject()
    {      
        if (movingObgectManager.ItemInventory == null)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            movingObgectManager.ItemInventory = CopyInventoryItem(items[currentID]);
            if(movingObgectManager.ItemInventory.id != 0)
            {
                movingObject.gameObject.SetActive(true);
                movingObject.GetComponent<Image>().sprite = data.items[movingObgectManager.ItemInventory.id].img;

                AddItem(currentID, data.items[0], 0);
            }
        }
        else
        {
            if (movingObgectManager.ItemInventory.id != 0)
            {
                    ItemInventory itemInventory = items[int.Parse(es.currentSelectedGameObject.name)];

                if (movingObgectManager.ItemInventory.id != itemInventory.id)
                {
                    if (itemInventory.id == 0)
                    {
                        AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), movingObgectManager.ItemInventory);
                    }
                    else
                    {
                        AddItem(currentID, data.items[movingObgectManager.ItemInventory.id], movingObgectManager.ItemInventory.count);
                    }
                }
                else
                {
                    if (itemInventory.count + movingObgectManager.ItemInventory.count <= cellSize)
                    {
                        itemInventory.count += movingObgectManager.ItemInventory.count;
                    }
                    else
                    {
                        AddItem(currentID, data.items[itemInventory.id], itemInventory.count + movingObgectManager.ItemInventory.count - cellSize);
                        itemInventory.count = cellSize;
                    }
                    if (itemInventory.id != 0)
                    {
                        itemInventory.itemGameObject.GetComponentInChildren<TMP_Text>().text = itemInventory.count.ToString();
                    }
                }
            }
            currentID = -1;
            movingObgectManager.ItemInventory = null;
            movingObject.gameObject.SetActive(false);
        }

        UpdateInventiory();
    }

    private void MoveObject()
    {
        movingObject.position = Input.mousePosition + offset;
    }
    private ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory
        {
            id = old.id,
            itemGameObject = old.itemGameObject,
            count = old.count
        };

        return New;
    }
    private void DeleteItem()
    {
        int index = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == 0)
            {
                index = i;
            }
        }
        movingObgectManager.ItemInventory = CopyInventoryItem(items[index]);
        currentID = -1;
        movingObgectManager.ItemInventory = null;
    }
    /// <summary>
    /// проверка что курсор находиться за границей UI
    /// </summary>
    /// <returns></returns>
    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void OpenInventory()
    {
        canvas.enabled = true;
        UpdateInventiory();
    }

    public void CloseInventory() 
    {
        canvas.enabled = false;
    }
}