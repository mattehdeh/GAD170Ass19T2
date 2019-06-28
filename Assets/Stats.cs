﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int level;
    public int health;
    public int speed;
    public int attack;
    public int defense;
    public int luck;

    public bool isDefeated;

    //public int health2, speed2, attack2, defense2, luck2;

    public enum StatusEffect
    {
        none,
        dizzy,
        poisoned,
        stunned
    }

    public StatusEffect myStatus;
    public StatusEffect attackEffect;

    public void Attacked(int incDmg, StatusEffect incEffect)
    {
        health -= incDmg - defense;
        myStatus = incEffect;
        if (health <= 0)
            isDefeated = true;
    }
}
