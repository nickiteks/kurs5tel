using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellBook : MonoBehaviour
{
    public DatabaseSpells dataSpells;

    public List<SpellInBook> spells = new List<SpellInBook>();

    [SerializeField]
    private GameObject gameObjectShow;

    [SerializeField]
    private GameObject InventoryMainObject;

    [SerializeField]
    private int maxCount;

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private EventSystem es;

    [SerializeField]
    private int currentID;

    [SerializeField]
    private RectTransform movingObject;
    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private GameObject backGround;

    [SerializeField]
    private int cellSize;

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
    /// <summary>
    /// добавление способности
    /// </summary>
    /// <param name="id"></param>
    /// <param name="spell"></param>
    /// <param name="count"></param>
    public void AddSpell(int id, Spell spell)
    {
        spells[id].id = spell.id;
        spells[id].spellGameObject.GetComponent<Image>().sprite = spell.img;
    }
    /// <summary>
    /// добавление уже существующего Spell в spellbook
    /// </summary>
    /// <param name="id"></param>
    /// <param name="spellInBook"></param>
    private void AddInventoryItem(int id, ItemInventory spellInBook)
    {
        spells[id].id = spellInBook.id;
        spells[id].spellGameObject.GetComponent<Image>().sprite = dataSpells.spells[spellInBook.id].img;
    }
    /// <summary>
    /// графика spellbook
    /// </summary>
    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newSpell = Instantiate(gameObjectShow, InventoryMainObject.transform) as GameObject;

            newSpell.name = i.ToString();
            SpellInBook spellInBook = new SpellInBook();
            spellInBook.spellGameObject = newSpell;

            RectTransform rt = newSpell.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newSpell.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newSpell.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            spells.Add(spellInBook);
        }
    }
    /// <summary>
    /// выбор обьекта в спеллбуке
    /// </summary>
    private void SelectObject()
    {
        //TODO нужная парням логика
    }
}
