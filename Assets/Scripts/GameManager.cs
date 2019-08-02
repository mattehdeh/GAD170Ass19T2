using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject defaultFoodPrefab;

    public List<GameObject> EnemySpawnList;
    public List<GameObject> EnemiesToFight;

    //0 = health, 1 = cur exp, 2 = level
    int[] storedPlayerStats;
    //or List<int> PlayerStats;
    Transform storedPlayerTransform;
    public string tracker;
    public enum Worlds
    {
        Overworld,
        BattleScene
    }

    //Awake is called before void Start on ANY OBJECT
    private void Awake()
    {
        foreach(GameObject gameMan in GameObject.FindGameObjectsWithTag("GameManager"))
        {
            if(gameMan.GetComponent<GameManager>().tracker != "sup")
            {
                Destroy(gameMan);
            }
        }
        //this will make it so we can travel between scenes (good for keeping track of gameplay!)
        DontDestroyOnLoad(this.gameObject);

    }

    public void TravelToWorld(Worlds destination)
    {
        switch (destination)
        {
            case Worlds.Overworld:
                //load overworld scene
                SavePlayerStuff(true);
                SceneManager.LoadScene("Overworld");
                LoadPlayerStuff(false);
                break;
            case Worlds.BattleScene:
                GenerateEnemies();
                tracker = "sup";
                SavePlayerStuff(false);
                //load battle scene
                SceneManager.LoadScene("BattleScene");
                LoadPlayerStuff(true);
                break;
        }
    }

    void GenerateEnemies()
    {
        //i < get component player plate size?
        for (int i = 0; i < 3; i++)
        {
            //add random enemies to fight from our list, this will run each time we enter the wild grass!
            EnemiesToFight.Add(EnemySpawnList[Random.Range(0, EnemySpawnList.Count)]);
        }
    }

    void SavePlayerStuff(bool isFromOverworld)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        //only save position in overworld
        if (isFromOverworld)
        {
            storedPlayerTransform.position = playerObj.transform.position;
            storedPlayerTransform.position = playerObj.transform.position;
        }

        //save stats that we need to track
        Stats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        storedPlayerStats[0] = (int)playerStats.health;
        storedPlayerStats[1] = playerStats.curExp;
        storedPlayerStats[2] = playerStats.level;
    }

    void LoadPlayerStuff(bool goingToOverworld)
    {
        //load the existing stats and applyu them to the player
        Stats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        playerStats.health = storedPlayerStats[0];
        playerStats.curExp = storedPlayerStats[1];
        playerStats.level = storedPlayerStats[2];
        //load posiiton only in the Overworld
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (goingToOverworld)
        {
            playerObj.transform.position = storedPlayerTransform.position;
            playerObj.transform.rotation = storedPlayerTransform.rotation;
        }
    }
}
