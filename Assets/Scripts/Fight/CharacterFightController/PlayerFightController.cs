using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerFightController : FightController
{
    private IUsable action = null;
    private EventSystem eventSystem;
    private Character character;

    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    public override void StartStep(Character character)
    {
        this.character = character;
        changeFightEvent.Invoke(FightState.ChoiceAction);
        UIFightManager.Instance.RefreshSpellsPanel(character.spellBook.ToArray());
    }

    public void SelectAction()
    {
        GameObject button = eventSystem.currentSelectedGameObject;
        action = character.spellBook[int.Parse(button.name)] as IUsable;
        
        changeFightEvent.Invoke(FightState.ChoiceTarget);
    }

    public void SelectTarget()
    {
        if (action == null) return;

        Character character = eventSystem.currentSelectedGameObject.GetComponentInParent<Character>();
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

        action = null;
        changeFightEvent.Invoke(FightState.EndStep);
    }

    public void CancelChoiceTarget()
    {
        action = null;
        changeFightEvent.Invoke(FightState.ChoiceAction);
    }
}
