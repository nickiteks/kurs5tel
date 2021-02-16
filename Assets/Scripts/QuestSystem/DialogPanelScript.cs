using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogPanelScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Текст при открытии панели")]
    private TMP_Text welcomeText;

    [SerializeField]
    [Tooltip("Контейнер заголовков привествия")]
    private TextContainer welcomeTexts;

    [Space(10)]

    [SerializeField]
    [Tooltip("Панель со списком заданий")]
    protected RectTransform panelListQuest;

    [SerializeField]
    [Tooltip("Панель выбранного задания")]
    protected RectTransform questPanel;

    [SerializeField]
    [Tooltip("Префаб кнопки выбора задания")]
    protected GameObject questButtonPrefub;

    /// <summary>
    /// Журнал заданий квестодателя
    /// </summary>
    public QuestMagazine QuestMagazine { private get; set; }


    [Tooltip("Система ивентов нажатия на кнопки")]
    protected EventSystem eventSystem;

    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    protected void OnEnable()
    {
        FillTextPanel();
        FillListQuestPanel();
    }

    private void OnDisable()
    {
        ClearListQuestPanel();
    }

    private void FillTextPanel()
    {
        if (welcomeTexts == null) return;
        welcomeText.text = welcomeTexts[Random.Range(0, welcomeTexts.Count)];
    }

    protected void FillListQuestPanel()
    {
        for (int i = 0; i < QuestMagazine.quests.Count; i++)
        {
            if (QuestMagazine.quests[i].CanCompleted)
            {
                GameObject questButton = Instantiate(questButtonPrefub, panelListQuest);

                questButton.name = i.ToString();
                questButton.GetComponentInChildren<TMP_Text>().text = QuestMagazine.quests[i].NameQuest;
                questButton.GetComponent<Button>().onClick.AddListener(delegate { SelectQuest(); });
            }
        }
    }

    private void ClearListQuestPanel()
    {
        var questButtons = panelListQuest.GetComponentsInChildren<Button>();

        foreach (Button button in questButtons)
        {
            Destroy(button.gameObject);
        }
    }

    protected void SelectQuest()
    {
        questPanel.GetComponent<QuestPanelScript>().Quest = QuestMagazine.quests[int.Parse(eventSystem.currentSelectedGameObject.name)];
        UIManager.Instance.OpenPanel(questPanel);
        UIManager.Instance.ClosePanel(GetComponent<RectTransform>());
    }
}
