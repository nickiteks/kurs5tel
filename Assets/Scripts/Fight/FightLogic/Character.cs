﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public List<SpellBook> spellBook { get; set; }
    public BaseStatsScript BaseStatsScript { get; set; }
    public bool IsEnemy { get; set; }
    
}