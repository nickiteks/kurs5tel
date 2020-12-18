using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightController : FightController
{
    public List<Character> opponents { get; set; }

    protected override void ApplyActionToTarget(Character target)
    {
        throw new System.NotImplementedException();
    }
}
