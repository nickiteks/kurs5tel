using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    public int id;
    public string name;
    public Sprite img;
    public int impact;
    public int manacost;

    public int Activate()
    {
        return impact;
    }   
}
