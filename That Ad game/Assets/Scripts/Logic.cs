using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public int Lives = 1;
    public GameObject player;
    public GameObject obsticlesPrefab;
    public GameObject playerPrefab;
    public GameObject EnemyPrefab;
    private bool go = false;

    private float timerObsticle = 0f;
    private float timerEnemy = 0f;
    public float spawnInterval = 5f;
    public float spawnEnemyInterval = 6f;
    private List<GameObject> players = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        players.Add(player);
    }

    void Update()
    {
        if (go)
        {
            timerEnemy += Time.deltaTime;
        }
        timerObsticle += Time.deltaTime;

        if (timerObsticle >= spawnInterval)
        {
            SpawnObstacle();
            timerObsticle = 0f;
            go = true;
        }
        if (timerEnemy >= spawnEnemyInterval)
        {
            SpawnEnemies();
            timerEnemy = 0f;
            go = false;
        }
        if (GameOver())
        {
            Time.timeScale = 0;
            Debug.Log("VÉGE");
        }
        Debug.Log("Élet: " + Lives + " Gat: " + timerObsticle + " Enemy: " + timerEnemy);
    }

    private void SpawnObstacle()
    {
        Vector3 spawnPosition = new Vector3(400f, 0.5f, -4.89094f);
        Instantiate(obsticlesPrefab, spawnPosition, Quaternion.identity);
    }

    private void SpawnEnemies()
    {
        //Vector3 spawnPosition = new Vector3(400f, 0.5f, Random.Range(-4.5f, -5f)/*-4.89094f*/);
        int numberOfEnemies = Lives + Random.Range(20, 50);
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(370f, 400f)/*400f*/, 0.5f, Random.Range(-10f, 5f)/*-4.89094f*/);
            Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
        }
        //Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void ApplyGateEffect(char operation, int value)
    {
        int newLives = Lives;

        switch (operation)
        {
            case '+': newLives += value; break;
            case '-': newLives -= value; break;
            case '*': newLives *= value; break;
            case '/': newLives /= value; break;
        }

        int difference = newLives - Lives;
        Lives = newLives;

        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                float numm1 = Random.Range(-0.2f, 0.2f);
                float numm2 = Random.Range(-0.2f, 0.2f);
                GameObject newPlayer = Instantiate(playerPrefab, player.transform.position + Vector3.forward * (1 * numm1) + Vector3.left * (1 * numm2), Quaternion.identity);
                players.Add(newPlayer);
            }
        }
        else if (difference < 0)
        {
            for (int i = 0; i < -difference; i++)
            {
                if (players.Count > 0)
                {
                    GameObject toRemove = players[players.Count - 1];
                    players.RemoveAt(players.Count - 1);
                    Destroy(toRemove);
                }
            }
        }
    }

    private bool GameOver()
    {
        return Lives <= 0;
    }
}
