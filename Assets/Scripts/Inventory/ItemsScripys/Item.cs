﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public int id;
    public string name;
    public Sprite img;
    public int armor;
    public int mana;
    public int damage;
    public int impact;
    public bool isSoloTarget;
    public bool IsEnemy;
}
