﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "Spell", menuName = "Spells/MassHeal")]
public class MassHeal : Spell, IUsable
{
    public int impact;
    public bool isSoloTarget;
    public bool IsEnemy;
    public bool useItem(Character[] target)
    {
        if ((isSoloTarget && target.Length != 1) || target.Length == 0) return false;
        if (target.FirstOrDefault(x => x.IsEnemy != this.IsEnemy)) return false;
        //TODO: дописать как раз влияние на харрактеристики
        return true;
    }
}
