using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestMagazinePlayerPanelScript : DialogPanelScript
{
    [SerializeField]
    private QuestMagazine questMagazine;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        QuestMagazine = questMagazine;
    }
}
