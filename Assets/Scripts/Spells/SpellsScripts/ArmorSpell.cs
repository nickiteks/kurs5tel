using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spells/ArmorSpell")]
public class ArmorSpell : Spell, IUsable
{
    public bool Use(Character[] target)
    {
        if ((isSoloTarget && target.Length != 1) || target.Length == 0) return false;
        if (target.FirstOrDefault(x => x.IsEnemy != IsEnemy) == null) return false;

        foreach (Character character in target)
        {
            character.BaseStatsScript.Armor += impact;
        }

        return true;
    }
}
