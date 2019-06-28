using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brief1Exp : MonoBehaviour
{
    public int enemyChoice = 0;
    public int playerChoice = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("To Start (press Space)");

    }

    void Choice()
    {

    }

    void RoShamBo()
    {
        Debug.Log("RO (press R) SHAM (press S) BO (press B)");
            enemyChoice = Random.Range(1, 4);
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerChoice = 1;
            Debug.Log("You chose Ro!");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RoShamBo();
        }
    }

}

    