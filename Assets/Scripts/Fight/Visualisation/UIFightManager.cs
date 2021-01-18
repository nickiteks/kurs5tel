using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFightManager : Singleton<UIFightManager>
{
    [SerializeField]
    private RectTransform choiseingActionPanel;
    [SerializeField]
    private RectTransform choisingTargetPanel;
    [SerializeField]
    private RectTransform winPanel;
    [SerializeField]
    private RectTransform endgamePanel;
    [SerializeField]
    private RectTransform itemPanel;
    [SerializeField]
    private RectTransform spellPanel;

    [Space(30)]

    [SerializeField]
    private GameObject button;

    private float heightScrollView = 0;
    private void Start()
    {
        RectTransform contentPanel = itemPanel.GetComponentInChildren<GridLayoutGroup>().GetComponent<RectTransform>();
        heightScrollView = contentPanel.rect.height;
    }

    public void OpenPanel(RectTransform panel)
    {
        panel.gameObject.SetActive(true);
    }

    public void ClosePanel(RectTransform panel)
    {
        panel.gameObject.SetActive(false);
    }

    public void OpenWinPanel()
    {
        CloseAllPanel();
        winPanel.gameObject.SetActive(true);
    }
    public void OpenActionPanel()
    {
        choiseingActionPanel.gameObject.SetActive(true);
    }
    public void CloseActionPanel()
    {
        choiseingActionPanel.gameObject.SetActive(false);
    }

    public void OpenTargetPanel()
    {
        choisingTargetPanel.gameObject.SetActive(true);
    }
    public void CloseTargetPanel()
    {
        choisingTargetPanel.gameObject.SetActive(false);
    }

    public void CloseAllPanel()
    {
        choiseingActionPanel.gameObject.SetActive(false);
        choisingTargetPanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(false);
        itemPanel.gameObject.SetActive(false);
        spellPanel.gameObject.SetActive(false);
        endgamePanel.gameObject.SetActive(false);
    }

    public void OpenEndGamePanel()
    {
        endgamePanel.gameObject.SetActive(true);
    }

    public void RefreshSpellsPanel(Spell[] spells)
    {
        GridLayoutGroup layoutGroup = spellPanel.GetComponentInChildren<GridLayoutGroup>();
        RectTransform contentPanel = spellPanel.GetComponentInChildren<GridLayoutGroup>().GetComponent<RectTransform>();

        var childs = contentPanel.GetComponentsInChildren<RectTransform>();
        for (int i = 1; i < childs.Length; i++)
        {
            Destroy(childs[i].gameObject);
        }

        int column = 1;
        for (int i = 0; i < spells.Length; i++)
        {
            GameObject spellButton = Instantiate(this.button, contentPanel);
            spellButton.name = i.ToString();

            spellButton.GetComponent<Image>().sprite = spells[i].img;

            Button button = spellButton.GetComponent<Button>();
            button.onClick.AddListener(delegate { (FightManager.Instance.playerController as PlayerFightController).SelectAction(); });
            button.onClick.AddListener(delegate { CloseAllPanel(); });
            button.onClick.AddListener(delegate { OpenTargetPanel(); });
            column++;
        }
    }

    public void RefreshItemsPanel()
    {
        RectTransform contentPanel = itemPanel.GetComponentInChildren<GridLayoutGroup>().GetComponent<RectTransform>();
        //contentPanel.sizeDelta = new Vector2(0, heightScrollView);
    }
}
