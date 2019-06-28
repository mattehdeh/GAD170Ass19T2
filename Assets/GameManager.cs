using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemyList;

    public List<GameObject> enemySpawnList;

    public enum GameState
    {
        notInCombat,
        InCombat
    }
    public GameState gameState;

    public enum CombatState
    {
        PlayerTurn,
        EnemyTurn,
        Victory,
        Loss
    }
    public CombatState combatState;

    //objects for combat
    public GameObject playerObj;
    public GameObject enemyObj;

    private bool doBattle = true;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyList.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doBattle)
        {
            StartCoroutine(battleGo());
            doBattle = false;
        }
    }
    public void DamageEnemies()
    {
        foreach (GameObject enemy in enemyList)
        {
            enemy.GetComponent<Stats>().health -= 10;
        }
    }
    public void HealEnemies()
    {
        foreach (GameObject enemy in enemyList)
        {
            enemy.GetComponent<Stats>().health += 10;
        }
    }
    public void RemoveEnemy(GameObject enemyToRemove)
    {
        enemyList.Remove(enemyToRemove);
    }
    public void SpawnEnemy()
    {
        //Spawn an enemy from our list of spawnable enemies
        //using the size of the list as the random range maximum
        Instantiate(enemySpawnList[Random.Range(0, enemySpawnList.Count)], transform);

    }

    public void CheckCombatState()
    {
        switch (combatState)
        {
            //Pseudo Code - code without code

            //Player Turn
            case CombatState.PlayerTurn:
                //Decision - Attack
                //Attack the Enemy
                BattleRound(playerObj, enemyObj);
                //Check if Enemy is defeated
                if (enemyObj.GetComponent<Stats>().isDefeated)
                    SpawnEnemy();
                //Next Case. Most Likely EnemyTurn
                combatState = CombatState.EnemyTurn;

                break;

            //Enemy Turn
            case CombatState.EnemyTurn:
                //Decision - Attack
                //Attack the PLayer
                BattleRound(enemyObj, playerObj);
                //Check if Player is defeated
                if (playerObj.GetComponent<Stats>().isDefeated)
                {
                    //set state to Lose cause we died
                    combatState = CombatState.Loss;
                    Debug.Log("Lose");
                    break;
                }
                //Next Case. Most Likely PlayerTurn
                combatState = CombatState.PlayerTurn;
                break;

            //Victory
            case CombatState.Victory:
                Debug.Log("You are win");

                break;

            //Tell Player they won
            //End Game

            case CombatState.Loss:
                //we lose, reset game
                SceneManager.LoadScene("SampleScene");
                break;


                //Loss
                //Tell Player they lost
                //Restart Game
        }
    }

    public void BattleRound(GameObject attacker, GameObject defender)
    {
        //will take an attacker and defender and make them do combat
        defender.GetComponent<Stats>().Attacked(attacker.GetComponent<Stats>().attack, Stats.StatusEffect.none);
        Debug.Log("Attacker: " + attacker.name + " | Defender: " + defender.name);
        Debug.Log(attacker.name +
            " attacks " +
            defender.name +
            " for a total of " +
            (attacker.GetComponent<Stats>().attack - defender.GetComponent<Stats>().defense) + " damage");
    }

    IEnumerator battleGo()
    {
        CheckCombatState();
        yield return new WaitForSeconds(1f);
        doBattle = true;

    }
}




