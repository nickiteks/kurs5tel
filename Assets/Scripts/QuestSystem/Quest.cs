using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Simple quest")]
public class Quest : ScriptableObject
{
    [SerializeField]
    [Tooltip("Название задания")]
    private string nameQuest;
    /// <summary>
    /// Название задания
    /// </summary>
    public string NameQuest { get { return nameQuest; } }

    [SerializeField]
    [TextArea(3, 8)]
    [Tooltip("Основной текст задания")]
    private string mainText;
    /// <summary>
    /// Основной текст задания
    /// </summary>
    public string MainText { get { return mainText; } }

    [SerializeField]
    [TextArea(3, 8)]
    [Tooltip("Краткое содержание задания")]
    private string quickText;
    public string QuickText { get { return quickText; } }

    /// <summary>
    /// Выполненно ли задание?
    /// </summary>
    [Tooltip("Выполненно ли задание?")]
    public bool isCompleted;

    /// <summary>
    /// Можно ли начать выполнение задания?
    /// </summary>
    public bool CanCompleted
    {
        get
        {
            if ((requirmentCompletedQuests.Count() == 0 || requirmentCompletedQuests == null) && !isCompleted) return true;
            else return requirmentCompletedQuests.FirstOrDefault(quest => !quest.isCompleted) == null && !isCompleted;
        }
    }

    [SerializeField]
    [Tooltip("Предметы необходимые для выполнения задания")]
    private Item[] neededItems;
    /// <summary>
    /// Предметы необходимые для выполнения задания
    /// </summary>
    public Item[] NeededItems { get { return neededItems; } }

    [SerializeField]
    [Tooltip("Вознаграждение за сдачу задания")]
    private Item[] rewardItems;
    /// <summary>
    /// Вознаграждение за сдачу задания
    /// </summary>
    public Item[] RewardItems { get { return rewardItems; } }

    [SerializeField]
    [Tooltip("Задания, которые необходимо выполнить, чтобы выполнить текущее задания")]
    private Quest[] requirmentCompletedQuests;
}
