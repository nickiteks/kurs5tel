using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class FightController : MonoBehaviour
{
    public FightState state 
    { 
        get=>state;
        private set 
        {
            changeFightEvent.Invoke(value);
            state = value;
        } 
    }
    public List<Character> characters { get; set; }
    public ChangeFightEvent changeFightEvent;

    public abstract void StartStep(Character character);

    protected abstract void ApplyActionToTarget(Character target);    
}
