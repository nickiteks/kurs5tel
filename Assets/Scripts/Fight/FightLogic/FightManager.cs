using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : Singleton<FightManager>
{
    private bool IsStepFriendly { get; set; }
    private FightController CurrentController;
    private FightController playerController;
    private FightController enemyController;

    private void ControlEndStep(FightState fightState)
    {
        //TODO:дописать
    }
    private void CreateScene()
    {
        //TODO:дописать
    }
    private void Start()
    {
        //TODO:дописать
    }
    private void CloseScene()
    {
        //TODO:дописать
    }
    private void ControlSwitchTurn()
    {
        //TODO:дописать
    }
}
