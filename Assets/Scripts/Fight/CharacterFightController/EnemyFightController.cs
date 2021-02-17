using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightController : FightController
{
    public List<Character> opponents { get; set; }

    public override void StartStep(Character character)
    {
        IUsable action = character.spellBook[Random.Range(0, character.spellBook.Count)] as IUsable;
        Spell spell = action as Spell;

        Debug.Log($"spell = {spell.name} caster name = {character.gameObject.name}");

        if (spell.isSoloTarget)
        {
            Character opponent = opponents[Random.Range(0, opponents.Count)];
            if (!action.Use(new Character[] { opponent }))
            {
                Debug.Log($"По игроку spell = {spell.name} enemy = {opponent.gameObject.name} - Ты обосрался!!!");
                return;
            }
        }
        else
        {
            if (!action.Use(opponents.ToArray()))
            {
                Debug.Log($"По игроку spell = {spell.name} all opponent - Ты обосрался!!!");
                return;
            }
        }

        changeFightEvent.Invoke(FightState.EndStep);
    }
}
