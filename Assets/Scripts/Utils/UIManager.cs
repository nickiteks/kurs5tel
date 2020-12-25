using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Inventory playerInventory;
    [SerializeField]
    private RectTransform switchScenePanel;

    public void OpenSwitchScenePanel()
    {
        switchScenePanel.gameObject.SetActive(true);
    }

    public void CloseSwitchScenePanel()
    {
        switchScenePanel.gameObject.SetActive(false);
    }

    public void OpenPlayerInventory()
    {
        playerInventory.OpenInventory();
    }

    public void ClosePlayerInventory()
    {
        playerInventory.CloseInventory();
    }

    public void OpenPanel(RectTransform rectTransform)
    {
        rectTransform.gameObject.SetActive(true);
    }

    public void ClosePanel(RectTransform rectTransform)
    {
        rectTransform.gameObject.SetActive(false);
    }
}
