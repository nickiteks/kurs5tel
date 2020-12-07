using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsSave", menuName = "StatsSave")]
public class StatsContainer : ScriptableObject
{

    public int manaMax;
    public int mana;
    public int health;
    public int maxHelth;
    public int armor;
    public int damage;
}
