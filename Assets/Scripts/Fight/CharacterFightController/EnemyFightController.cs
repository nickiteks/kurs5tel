using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightController : FightController
{
    public List<Character> opponents { get; set; }

    public override void StartStep(Character character)
    {
        throw new System.NotImplementedException();
    }

    protected override void ApplyActionToTarget(Character target)
    {
        throw new System.NotImplementedException();
    }
}
