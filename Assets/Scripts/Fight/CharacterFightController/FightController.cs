using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class FightController : MonoBehaviour
{
    private FightState state 
    { 
        get=>state;
        set 
        {
            changeFightEvent.Invoke(value);
            state = value;
        } 
    }
    public List<Character> characters { get; set; }
    private ChangeFightEvent changeFightEvent;
    public void StartStep(Character character)
    {

    }
    protected abstract void ApplyActionToTarget(Character target);
    
    public void SubscribitionChangeFightStateEvent(UnityAction action)
    {

    }
    public void UnSubscribitionChangeFightStateEvent(UnityAction action)
    {

    }    
}
