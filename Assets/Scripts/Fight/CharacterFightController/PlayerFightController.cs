using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerFightController : FightController
{
    private IUsable action = null;
    private EventSystem eventSystem;
    private Character character;
    [SerializeField]
    private Inventory inventory;
    private Logger logger = new Logger("fightActions");

    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    public override void StartStep(Character character)
    {
        this.character = character;
        changeFightEvent.Invoke(FightState.ChoiceAction);
        UIFightManager.Instance.RefreshSpellsPanel(character.spellBook.ToArray());
        UIFightManager.Instance.RefreshItemsPanel();
    }

    public void SelectAction()
    {
        GameObject button = eventSystem.currentSelectedGameObject;
        if (MovingObgectManager.Instance.ItemInventory == null)
        {
            if (CheckSpellValidation.CheckSpellExists(button.name, character))
            {
                action = character.spellBook[int.Parse(button.name)] as IUsable;
                logger.Log(action.ToString() + "|" + DateTime.Now);
            }
        }
        else
        {
            
            if (inventory.data.items[MovingObgectManager.Instance.ItemInventory.id] is IUsable)
            {
                action = inventory.data.items[MovingObgectManager.Instance.ItemInventory.id] as IUsable;
                Debug.Log(action);
                UIFightManager.Instance.CloseAllPanel();
                UIFightManager.Instance.OpenTargetPanel();
            }
            else
            {
                inventory.AddInventoryItem(int.Parse(button.name), MovingObgectManager.Instance.ItemInventory);
                MovingObgectManager.Instance.ItemInventory = null;
            }
            MovingObgectManager.Instance.ItemInventory = null;
        }
        
        changeFightEvent.Invoke(FightState.ChoiceTarget);
    }

    public void SelectTarget()
    {
        if (action == null) return;
        Character character = eventSystem.currentSelectedGameObject.GetComponentInParent<Character>();
        
        if (action is Spell)
        {
            Spell spell = action as Spell;
            if (spell.isSoloTarget)
            {
                if (!action.Use(new Character[] { character })) return;
            }
            else
            {
                if (!character.IsEnemy)
                {
                    if (!action.Use(FightManager.Instance.playerController.characters.ToArray())) return;
                }
                else
                {
                    if (!action.Use(FightManager.Instance.enemyController.characters.ToArray())) return;
                }

            }
        } 
        else
        {
            
            Item item = action as Item;
            if (item.isSoloTarget)
            {
                if (!action.Use(new Character[] { character })) return;
            }
            else
            {
                if (!character.IsEnemy)
                {
                    if (!action.Use(FightManager.Instance.playerController.characters.ToArray())) return;
                }
                else
                {
                    if (!action.Use(FightManager.Instance.enemyController.characters.ToArray())) return;
                }

            }
        }

        

        action = null;
        changeFightEvent.Invoke(FightState.EndStep);
    }

    public void CancelChoiceTarget()
    {
        action = null;
        MovingObgectManager.Instance.ItemInventory = null;
        changeFightEvent.Invoke(FightState.ChoiceAction);
    }
}
