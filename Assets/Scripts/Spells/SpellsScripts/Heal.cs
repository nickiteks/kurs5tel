using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spells/Heal")]
public class Heal : Spell, IUsable
{
    public bool Use(Character[] target)
    {
        if ((isSoloTarget && target.Length != 1) || target.Length == 0) return false;
        if (target.FirstOrDefault(x => x.IsEnemy != this.IsEnemy) != null) return false;
        foreach (Character character in target)
        {
            character.BaseStatsScript.Health += impact;
        }
        return true;
    }
}
