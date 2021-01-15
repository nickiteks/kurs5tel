using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Inventory playerInventory;
    bool isIventoryOpen = false;
    [SerializeField]
    private RectTransform switchScenePanel;

    private void Update()
    {
        if (Input.GetKeyDown(InputManager.Instance.inventoryOpen) && !isIventoryOpen) OpenPlayerInventory();
        else if (Input.GetKeyDown(InputManager.Instance.inventoryOpen) && isIventoryOpen) ClosePlayerInventory();     
    }
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
        isIventoryOpen = true;
    }

    public void ClosePlayerInventory()
    {
        playerInventory.CloseInventory();
        isIventoryOpen = false;
    }

    public void OpenPanel(RectTransform rectTransform)
    {
        rectTransform.gameObject.SetActive(true);
    }

    public void ClosePanel(RectTransform rectTransform)
    {
        rectTransform.gameObject.SetActive(false);
    }

    public void CloseCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(false);
    }

    public void OpenCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);
    }
}
