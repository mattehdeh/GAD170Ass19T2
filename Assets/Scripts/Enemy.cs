using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Stats myStats;
    public int flavourSome;

    private GameObject BattleManager;

    public enum flavourFull
    {
        sweet,
        sour,
        salty,
        bitter
    }

    public enum morselSize
    {
        small,
        medium,
        large
    }

    public morselSize mySize;
    public flavourFull myFlavour;

    void Start()
    {
        BattleManager = GameObject.FindGameObjectWithTag("BattleManager");

        myStats = GetComponent<Stats>();
        //Stats compenent must exist on the object!

       switch (mySize)
        {
            case morselSize.small:
                flavourSome = 1;
                for (int i = 0; i < flavourSome; i++)
                {
                    myFlavour = (flavourFull)Random.Range(0, 3);
                }
                break;
            case morselSize.medium:
                flavourSome = 2;
                for (int i = 0; i < flavourSome; i++)
                {
                    myFlavour = (flavourFull)Random.Range(0, 3);
                }
                break;
            case morselSize.large:
                flavourSome = 3;
                for (int i = 0; i < flavourSome; i++)
                {
                    myFlavour = (flavourFull)Random.Range(0, 3);
                }
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
    

    public void Defeated()
    {
        BattleManager.GetComponent<BattleManager>().RemoveEnemy(gameObject);
    }
    */
}