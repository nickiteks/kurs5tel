using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanelScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Название задания")]
    private TMP_Text headerText;

    [SerializeField]
    [Tooltip("Главный текст задания")]
    private TMP_Text mainText;

    [SerializeField]
    [Tooltip("Краткое содержание задания")]
    private TMP_Text quickText;

    [Space(10)]

    [SerializeField]
    [Tooltip("Панель для отображения награды")]
    private RectTransform rewardItemsPanel;

    [Tooltip("Список заданий игрока")]
    private QuestMagazine questMagazinePlayer;

    [Tooltip("Инвентарь игрока")]
    private Inventory inventory;
    /// <summary>
    /// Открытое задание
    /// </summary>
    public Quest Quest { private get; set; }

    [SerializeField]
    [Tooltip("Кнопка сдачи задания")]
    private Button submitedQuestButton;

    [SerializeField]
    [Tooltip("Кнопка взятия задания")]
    private Button selectedQuestButton;

    private Logger logger;

    private void Awake()
    {
        logger = new Logger("Quest");
        questMagazinePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestMagazine>();
        inventory = GameObject.FindGameObjectWithTag("PlayerInventory").GetComponent<Inventory>();
    }

    private void OnEnable()
    {
        FillAllTexts();
        FillRewardItemsPanel();
        if (submitedQuestButton && selectedQuestButton) OnEnableNeededButton();
    }

    private void OnDisable()
    {
        ClearRewardItemsPanel();
        if (submitedQuestButton && selectedQuestButton)
        {
            submitedQuestButton.gameObject.SetActive(false);
            selectedQuestButton.gameObject.SetActive(false);
        }
            
    }

    private void ClearRewardItemsPanel()
    {
        var rewardItems = (rewardItemsPanel.GetComponentsInChildren<Image>()).ToList();
        rewardItems.RemoveAt(0);

        foreach (var item in rewardItems)
        {
            Destroy(item.gameObject);
        }

    }

    private void OnEnableNeededButton()
    {

        if (questMagazinePlayer.quests.Contains(Quest))
        {
            if (CheckCompletedQuest())
                submitedQuestButton.gameObject.SetActive(true);
        }
        else
        {
            selectedQuestButton.gameObject.SetActive(true);
        }
    }

    private bool CheckCompletedQuest()
    {
        foreach (Item item in Quest.NeededItems)
        {
            if (inventory.items.FirstOrDefault(itemInv => itemInv.id == item.id) == null) return false;
        }

        return true;
    }

    private void FillAllTexts()
    {
        headerText.text = Quest.NameQuest;
        mainText.text = Quest.MainText;
        quickText.text = Quest.QuickText;
    }

    private void FillRewardItemsPanel()
    {
        foreach (var item in Quest.RewardItems)
        {
            GameObject rewardItem = new GameObject("item");
            Image itemImage = rewardItem.AddComponent<Image>();
            itemImage.sprite = item.img;

            Instantiate(rewardItem, rewardItemsPanel);
        }
    }

    /// <summary>
    /// Сдать задание
    /// </summary>
    public void SubmitQuest()
    {
        questMagazinePlayer.quests.Remove(Quest);

        foreach (Item item in Quest.NeededItems)
        {
            inventory.items[inventory.items.IndexOf(inventory.items.FirstOrDefault(itemInv => itemInv.id == item.id))].id = 0;
        }

        foreach (Item item in Quest.RewardItems)
        {
            inventory.AddItem(item, 1);
        }

        logger.Log("SubmitQuest - " + Quest.NameQuest);
        Quest.isCompleted = true;
    }

    /// <summary>
    /// Взять задание
    /// </summary>
    public void TakeQuest()
    {
        logger.Log("SelectQuest - " + Quest.NameQuest);
        questMagazinePlayer.quests.Add(Quest);
    }
}
