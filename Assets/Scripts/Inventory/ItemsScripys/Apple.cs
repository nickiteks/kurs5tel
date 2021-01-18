using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Apple", menuName = "Items/Apple")]
public class Apple : Item, IUsable
{

    public bool Use(Character[] target)
    {
        if((isSoloTarget && target.Length != 1) || target.Length == 0) return false;
        if (target.FirstOrDefault(x => x.IsEnemy != IsEnemy) != null) return false;

        foreach (Character character in target)
        {
            character.BaseStatsScript.Health += impact;
        }

        return true;
    }
}
