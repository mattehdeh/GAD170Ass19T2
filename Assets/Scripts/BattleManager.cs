using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
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
    private GameObject gameManager;
    private GameObject battleUIManager;
    private bool doBattle = false;

    //events only need types, not variable names
    public event System.Action<bool, float> UpdateHealth;

    private void Awake()
    {
        //sub to BattleManager
        battleUIManager = GameObject.FindGameObjectWithTag("BattleUIManager");
        battleUIManager.GetComponent<BattleUIManager>().CallAttack += CheckCombatState;
        battleUIManager.GetComponent<BattleUIManager>().CallDefend += CheckCombatState;
        battleUIManager.GetComponent<BattleUIManager>().CallHeal += CheckCombatState;
        //You would probably have an enum called 'playerDecision" which woudl keep track
        //of whatever button was pressed (decision amde) and then call CheckCombatState using that
        //on the players turn automatically run during the enemies turn but turn it bacvk to manual
        //during the players turn (you can use coroutines and bools to handle this!)
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyList.Add(enemy);
        }
        //find our gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        //testing UI
        //UpdateHealth(true, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (doBattle)
        {
            //Set turn based on playerObj speed and enemyObj speed
            //Fastest should go first, random if same
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
                //You will want to assign exp here, before you travel, otherwise you coule have an issue where you dont get your reward
                //Travels out to the Overworld
                gameManager.GetComponent<GameManager>().TravelToWorld(GameManager.Worlds.Overworld);
                break;
                

            case CombatState.Loss:
                Debug.Log("You Are Lose");
                //we lose, travel to Overworld
                gameManager.GetComponent<GameManager>().TravelToWorld(GameManager.Worlds.Overworld);
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
            (attacker.GetComponent<Stats>().attack - defender.GetComponent<Stats>().defense) + 
            " damage");
        //setup temporary float value for fill amount (0.0f - 1.0f) bu simply dividing current health by max health
        float percentage = defender.GetComponent<Stats>().health / defender.GetComponent<Stats>().maxHealth;
        //debug for reasons
        Debug.Log(percentage);
        //
        //UpdateHealth(combatState == CombatState.PlayerTurn, percentage);

    }

    IEnumerator battleGo()
    {
        CheckCombatState();
        yield return new WaitForSeconds(1f);
        doBattle = true;

    }
}




