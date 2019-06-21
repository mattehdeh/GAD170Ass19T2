using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScript1 : MonoBehaviour
{

    public int playerLives = 3;
    public int HPnow = 10;
    public int HPmax = 20;
    public int Attacks = 2;

    public int attendanceRate;
    public float attending = 8;
    public float classSize = 12;

    public bool classCounted;

    // Start is called before the first frame update
    void Start()
    {
        //HelloWorld();
        //IncreaseLives(2); //make this ModifyLives and you can subtract as well
        //THis function requires an integer, when we set that, it can act
        DisplayName("Matty", "Davis");
        DisplayHealth(HPnow, HPmax);
        UpdateHealth(-2);
        DisplayHealth(HPnow, HPmax);
        attendanceRate = Mathf.RoundToInt(attending / classSize * 100);
        Debug.Log("Attendance %: " + attendanceRate);

     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //very simple coin toss
            //1 is winner

            //if(successCalc > 60) <-- could be 75 or 100. The variable successCalc is determined by player stats
            //make it more interesting with a (random.range(successCalc,100) >75)

            if (Random.Range(1, 7) >4)
            {
                HelloWorld();
                ModifyLives(2);
            }
            else
            {
                Debug.Log("Lost the coin toss");
                ModifyLives(-2);
            }
            
        }
        if(!classCounted)
        {
            for (int i = 0; i < classSize; i++)
            {
                Debug.Log(i);
            }
            classCounted = true;
        }
        






    }        
//Debug.Log(incLives); //this is an error because that variable only exists in the function
    void HelloWorld()
    {
        Debug.Log("Hello World");

    }
    void ModifyLives(int incLives) //This is a local variable because it is local to the function
    {
        playerLives += incLives;
        //Debug.Log(incLives); //this works
    }
    void DisplayName(string firstName, string lastName)
    {
        Debug.Log(firstName + " " + lastName);
    }

    void DisplayHealth(int currentHP, int maxHP)
    {
        Debug.Log(currentHP + "/" + maxHP);
    }
    void UpdateHealth(int incDamage)
    {
        HPnow += incDamage;
    }

}

