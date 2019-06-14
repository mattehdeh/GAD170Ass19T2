using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Stats myStats;

    public enum EnemyTypes
    {
        small,
        medium,
        large
    }

    public EnemyTypes myType;



    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        myStats.health = 45;

    }

    public void Attacked(int incDmg)
    {
        myStats.health -= incDmg - myStats.defense;
    }
}