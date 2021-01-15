using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spells/AccurateShot")]
public class AccurateShot : Spell, IUsable
{
    public int impact;
    public bool isSoloTarget;
    public bool IsEnemy;
    public bool useItem(Character[] target)
    {
        if ((isSoloTarget && target.Length != 1) || target.Length == 0) return false;
        if (target.FirstOrDefault(x => x.IsEnemy != IsEnemy) == null) return false;
        //TODO: дописать как раз влияние на харрактеристики
        return true;
    }
}
