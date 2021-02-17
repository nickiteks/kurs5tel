using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class FightController : MonoBehaviour
{
    [System.NonSerialized]
    public List<Character> characters = new List<Character>();
    public ChangeFightEvent changeFightEvent;

    public abstract void StartStep(Character character);
  
}
