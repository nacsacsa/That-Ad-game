using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.HDROutputUtils;

public class Logic : MonoBehaviour
{
    public int Lives = 1;
    public GameObject player;
    public GameObject obsticlesPrefab;
    public GameObject playerPrefab;
    public GameObject EnemyPrefab;
    private bool go = false;
    private bool stageWin = false;
    private int numberOfEnemies = 1;

    private float timerObsticle = 0f;
    private float timerEnemy = 0f;
    public float spawnInterval = 5f;
    public float spawnEnemyInterval = 6f;
    private List<GameObject> players = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();

    private int helyesOsszeadas;
    private int helyesSzorzas;
    private int helytelenOsszeadas;
    private int helyteleSzorzas;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI enemyText;

    void Start()
    {
        players.Add(player);
        SpawnObstacle();
    }

    void Update()
    {
        if (stageWin)
        {
            timerObsticle += Time.deltaTime;
        }
        if (go)
        {
            timerEnemy += Time.deltaTime;
        }
        if (timerObsticle >= spawnInterval)
        {
            SpawnObstacle();
        }
        if (timerEnemy >= spawnEnemyInterval)
        {
            SpawnEnemies();
        }
        if (GameOver())
        {
            Time.timeScale = 0;
            Debug.Log("VÉGE");
        }
        Debug.Log("Élet: " + Lives + " Gat: " + timerObsticle + " Enemy: " + enemies.Count);

        hpText.text = "HP: " + Lives;
        enemyText.text = "Enemies: " + numberOfEnemies;
    }

    private void SpawnObstacle()
    {
        timerObsticle = 0f;
        go = true;
        numberOfEnemies = Lives + Random.Range(10, 30);
        CalculateGateNumbers();
        Vector3 spawnPosition = new Vector3(400f, 0.5f, -4.89094f);
        GameObject obstacleObject = Instantiate(obsticlesPrefab, spawnPosition, Quaternion.identity);

        Transform leftGate = obstacleObject.transform.Find("Gate Left");
        Transform rightGate = obstacleObject.transform.Find("Gate Right");

        bool correctOnLeft = Random.value < 0.5f;

        bool correctIsAddition = Random.value < 0.5f;
        int correctValue = correctIsAddition ? helyesOsszeadas : helyesSzorzas;
        char correctOp = correctIsAddition ? '+' : '*';

        bool incorrectIsAddition = Random.value < 0.5f;
        int incorrectValue = incorrectIsAddition ? helytelenOsszeadas : helyteleSzorzas;
        char incorrectOp = incorrectIsAddition ? '+' : '*';

        if (leftGate != null)
        {
            GateValueCallculator leftScript = leftGate.GetComponent<GateValueCallculator>();
            if (leftScript != null)
            {
                if (correctOnLeft)
                {
                    leftScript.number = correctValue;
                    leftScript.operation = correctOp;
                }
                else
                {
                    leftScript.number = incorrectValue;
                    leftScript.operation = incorrectOp;
                }
            }
        }

        if (rightGate != null)
        {
            GateValueCallculator rightScript = rightGate.GetComponent<GateValueCallculator>();
            if (rightScript != null)
            {
                if (correctOnLeft)
                {
                    rightScript.number = incorrectValue;
                    rightScript.operation = incorrectOp;
                }
                else
                {
                    rightScript.number = correctValue;
                    rightScript.operation = correctOp;
                }
            }
        }
    }

    private void CalculateGateNumbers()
    {
        int szam = 0;
        szam = Random.Range(1, numberOfEnemies * 2);
        while (Lives * szam < numberOfEnemies)
        {
            szam = Random.Range(1, numberOfEnemies * 2);
        }
        helyesSzorzas = szam;
        szam = Random.Range(1, numberOfEnemies * 2);
        while (Lives + szam < numberOfEnemies)
        {
            szam = Random.Range(1, numberOfEnemies * 2);
        }
        helyesOsszeadas = szam;
        szam = Random.Range(1, numberOfEnemies * 2);
        while (!(Lives * szam < numberOfEnemies))
        {
            szam = Random.Range(1, numberOfEnemies * 2);
        }
        helyteleSzorzas = szam;
        szam = Random.Range(1, numberOfEnemies * 2);
        while (!(Lives + szam < numberOfEnemies))
        {
            szam = Random.Range(1, numberOfEnemies * 2);
        }
        helytelenOsszeadas = szam;
    }

    private void SpawnEnemies()
    {
        timerEnemy = 0f;
        go = false;
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(380f, 400f), 0.5f, Random.Range(-10f, 5f));
            GameObject newEnemy = Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
            enemies.Add(newEnemy);
        }
    }

    public void ApplyGateEffect(char operation, int value)
    {
        int newLives = Lives;

        switch (operation)
        {
            case '+': newLives += value; break;
            case '*': newLives *= value; break;
        }

        int difference = newLives - Lives;
        Lives = newLives;

        for (int i = 0; i < difference; i++)
        {
            float numm1 = Random.Range(-0.2f, 0.2f);
            float numm2 = Random.Range(-0.2f, 0.2f);
            GameObject newPlayer = Instantiate(playerPrefab, player.transform.position + Vector3.forward * (1 * numm1) + Vector3.left * (1 * numm2), Quaternion.identity);
            players.Add(newPlayer);
        }
    }

    private bool GameOver()
    {
        return Lives <= 0;
    }
}
