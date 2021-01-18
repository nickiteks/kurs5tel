using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    public int id;
    public new string name;
    public Sprite img;
    public int manacost;
    public bool isSoloTarget;
    public bool IsEnemy;
    public int impact;
}
