using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMagazine : MonoBehaviour
{
    [Tooltip("Список заданий")]
    public List<Quest> quests;

    [System.NonSerialized]
    [Tooltip("Выбранное для просмотра задания")]
    public Quest selectedQuest;
}
