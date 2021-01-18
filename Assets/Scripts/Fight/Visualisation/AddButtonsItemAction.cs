using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddButtonsItemAction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(delegate { (FightManager.Instance.playerController as PlayerFightController).SelectAction(); });
        }
    }
}
