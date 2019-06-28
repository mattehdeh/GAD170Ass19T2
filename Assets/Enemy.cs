using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Stats myStats;
    public Experience myExperience;

    private GameObject GameManager;

    public enum EnemyTypes
    {
        small,
        medium,
        large
    }

    public EnemyTypes myType;



    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");

        myStats = GetComponent<Stats>();
        //Stats compenent must exist on the object!

       switch (myType)
        {
            case EnemyTypes.small:
                //do setup
                break;
            case EnemyTypes.medium:
                //do thing
                break;
            case EnemyTypes.large:
                //do it
                break;
        }

    }
    /*
    public void Attacked(int incDmg, Stats.StatusEffect incEffect)
    {
        myStats.health -= incDmg - myStats.defense;
        myStats.myStatus = incEffect;
        if (myStats.health <= 0)
            myStats.isDefeated = true;
    }
    
    public void AttackTarget(GameObject target)
    {
        target.GetComponent<Player>().Attacked(myStats.attack, Stats.StatusEffect.none);
    }
    */

    public void Defeated()
    {
        GameManager.GetComponent<GameManager>().RemoveEnemy(gameObject);
    }
}