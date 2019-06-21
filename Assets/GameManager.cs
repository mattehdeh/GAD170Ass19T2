using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        switch(combatState)
        {
            //Pseudo Code - code without code

            //Player Turn
            case CombatState.PlayerTurn:
                //Decision - Attack
                //Attack the Enemy
                playerObj.GetComponent<Player>().AttackTarget(enemyObj);
                //Check if Enemy is defeated
                if (enemyObj.GetComponent<Enemy>().myStats.isDefeated)
                    SpawnEnemy();
                //Next Case. Most Likely EnemyTurn
                break;

            //Enemy Turn
            case CombatState.EnemyTurn:
                //Decision - Attack
                //Attack the PLayer
                enemyObj.GetComponent<Enemy>().AttackTarget(enemyObj);
                //Check if Player is defeated
                //Next Case. Most Likely PlayerTurn
                if (playerObj.GetComponent<Player>().myStats.isDefeated)
                    //restart game
                    break;            

                //Victory
                //Tell Player they won
                //End Game

                //Loss
                //Tell Player they lost
                //Restart Game
        }
    }

