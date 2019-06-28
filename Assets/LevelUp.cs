using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    //Xp given from interaction
    public int XPAwarded;
    //Xp gained so far
    public int XPHave = 0;
    //Total Xp needed for next level up
    public int XPNeed;

    // Start is called before the first frame update
    void Start()
    {
        XPNeed = (Player.GetComponent<Stats>().level ^ 1.05 * 15 + 10;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
