using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenInventoryCorpseInteraction : MonoBehaviour, IInteractive
{
    [SerializeField]
    private Inventory enemyInventory;
    private PlayerController playerController;

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private TMP_Text tmpText;

    [SerializeField]
    private Button buttonCloseInventory;
    private Logger logger;
    private void Awake()
    {
        canvas.enabled = false;
        buttonCloseInventory.onClick.AddListener(delegate { CancelAction(); });

        tmpText.text = InputManager.Instance.interaction.ToString();
        logger = new Logger("interactionLogs");
    }

    public void Action()
    {
        enemyInventory.OpenInventory();
        UIManager.Instance.OpenPlayerInventory();
        logger.Log("Script:OpenInventoryCorpseInteraction" + " GameObject:" + gameObject.name + " Position:" + transform.position);
    }

    public void CancelAction()
    {
        enemyInventory.CloseInventory();
        canvas.enabled = true;
        if (playerController) playerController.IsInteractionWithWorld = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerController = collision.GetComponent<PlayerController>())
        {
            canvas.enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (playerController && Input.GetKeyDown(InputManager.Instance.interaction) && canvas.enabled)
        {
            playerController.IsInteractionWithWorld = true;
            canvas.enabled = false;
            Action();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelAction();
        canvas.enabled = false;
    }
}
