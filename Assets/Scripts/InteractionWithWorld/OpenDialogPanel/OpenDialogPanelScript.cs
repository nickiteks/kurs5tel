using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenDialogPanelScript : MonoBehaviour, IInteractive
{
    [SerializeField]
    [Tooltip("Панель диалога")]
    private Canvas dialogCanvas;

    [SerializeField]
    [Tooltip("Панель с отображением кнопки взаимодействия")]
    private Canvas canvasWithActionButton;

    [SerializeField]
    [Tooltip("Наименование кнопки взаимодействия")]
    private TMP_Text tmpText;

    [Tooltip("Контроллер игрока")]
    private PlayerController playerController;

    [SerializeField]
    [Tooltip("Кнопка закрытия диалогового окна")]
    private Button buttonCloseDialogCanvas;

    [SerializeField]
    [Tooltip("Список заданий квестодателя")]
    private QuestMagazine questMagazine;

    private void Awake()
    {
        canvasWithActionButton.enabled = false;
        buttonCloseDialogCanvas.onClick.AddListener(delegate { CancelAction(); });

        tmpText.text = InputManager.Instance.interaction.ToString();
    }

    public void Action()
    {
        dialogCanvas.GetComponentInChildren<DialogPanelScript>().QuestMagazine = questMagazine;
        UIManager.Instance.OpenCanvas(dialogCanvas);
    }

    public void CancelAction()
    {
        UIManager.Instance.CloseCanvas(dialogCanvas);
        canvasWithActionButton.enabled = true;
        if (playerController) playerController.IsInteractionWithWorld = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerController = collision.GetComponent<PlayerController>())
        {
            canvasWithActionButton.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerController && Input.GetKeyDown(InputManager.Instance.interaction))
        {
            playerController.IsInteractionWithWorld = true;
            canvasWithActionButton.enabled = false;
            Action();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelAction();
        canvasWithActionButton.enabled = false;
    }
}
