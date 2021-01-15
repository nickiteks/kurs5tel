using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFightController : FightController
{
    public Inventory Inventory { get; set; }
    private IUsable action;

    public override void StartStep(Character character)
    {
        throw new System.NotImplementedException();
    }

    public void SelectAction()
    {

    }
    public void SelectTarget()
    {

    }
    public void CancelChoiceTarget()
    {

    }

    protected override void ApplyActionToTarget(Character target)
    {
        throw new System.NotImplementedException();
    }
}
