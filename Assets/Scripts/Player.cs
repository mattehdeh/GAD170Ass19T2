using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Stats myStats;
    public Experience myExperience;

    public int plateSize { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<Stats>();
        myExperience = GetComponent<Experience>();
    }

    
    /*
    public void AttackTarget(GameObject target)
    {
        target.GetComponent<Enemy>().Attacked(myStats.attack, Stats.StatusEffect.none);
    }
    */
}
