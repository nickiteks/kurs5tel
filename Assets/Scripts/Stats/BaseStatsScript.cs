﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseStatsScript : MonoBehaviour
{
    [SerializeField]
    private int manaMax;
    [SerializeField]
    private int mana;
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHelth;
    [SerializeField]
    private int armor;
    [SerializeField]
    private int damage;
    public int id { get; set; }

    public int Mana
    {
        get
        {
            return mana;
        }
        set
        {
            mana = value;
        }
    }
    public int ManaMax
    {
        get
        {
            return manaMax;
        }
        set
        {
            manaMax = value;
        }
    }
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health <= 0)
            {
                Death();
            }
            if (health > maxHelth)
            {
                health = maxHelth;
            }
        }
    }
    public void SetDamage(int damage)
    {
        Health -= (damage - armor);
    }
    private void Death()
    {
        Destroy(gameObject);
    }
    public int MaxHelth
    {
        get
        {
            return maxHelth;
        }
        set
        {
            maxHelth = value;
        }
    }
    public int Armor
    {
        get
        {
            return armor;
        }
        set
        {
            armor = value;
        }
    }
    private void Awake()
    {
        health = maxHelth;
    }
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
}
