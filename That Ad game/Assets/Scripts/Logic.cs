using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Logic : MonoBehaviour
{
    [Header("Prefabs & References")]
    public GameObject player;
    public GameObject obsticlesPrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI enemyText;

    [Header("Game Settings")]
    public int Lives = 1;
    public float spawnInterval = 5f;
    public float spawnEnemyInterval = 6f;

    private bool isEnemySpawnable = false;
    private bool isFight = false;
    private bool isRun = false;
    private bool isFighting = false;
    private bool isStart = true;

    private float obstacleTimer = 0f;
    private float enemyTimer = 0f;
    private float fightCounter = 0f;

    private int numberOfEnemies = 1;

    private List<GameObject> players = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();

    private int correctAddition;
    private int correctMultiplication;
    private int incorrectAddition;
    private int incorrectMultiplication;

    void Start()
    {
        players.Add(player);
        SpawnObstacle();
        isRun = true;
    }

    void Update()
    {
        HandleRunLogic();
        HandleFightLogic();
        UpdateUI();
        CheckGameOver();
    }

    private void HandleRunLogic()
    {
        if (!isRun) return;

        obstacleTimer += Time.deltaTime;

        if (obstacleTimer >= spawnInterval)
        {
            SpawnObstacle();
        }
    }

    private void HandleFightLogic()
    {
        if (numberOfEnemies == 0 && !isStart)
        {
            isFight = false;
            isRun = true;
        }

        if (isFight)
        {
            fightCounter += Time.deltaTime;
            if (fightCounter > 3f)
            {
                isFighting = true;
                isFight = false;
                fightCounter = 0f;
            }
        }

        if (isEnemySpawnable)
        {
            enemyTimer += Time.deltaTime;
            if (enemyTimer >= spawnEnemyInterval)
            {
                SpawnEnemies();
            }
        }
    }

    private void SpawnObstacle()
    {
        obstacleTimer = 0f;
        isEnemySpawnable = true;
        isRun = false;
        isStart = false;

        numberOfEnemies = Lives + Random.Range(10, 30);
        CalculateGateValues();

        Vector3 spawnPos = new Vector3(400f, 0.5f, -4.89094f);
        GameObject obstacleObj = Instantiate(obsticlesPrefab, spawnPos, Quaternion.identity);

        Transform leftGate = obstacleObj.transform.Find("Gate Left");
        Transform rightGate = obstacleObj.transform.Find("Gate Right");

        bool correctOnLeft = Random.value < 0.5f;
        bool correctIsAddition = Random.value < 0.5f;
        char correctOp = correctIsAddition ? '+' : '*';
        int correctValue = correctIsAddition ? correctAddition : correctMultiplication;

        bool incorrectIsAddition = Random.value < 0.5f;
        char incorrectOp = incorrectIsAddition ? '+' : '*';
        int incorrectValue = incorrectIsAddition ? incorrectAddition : incorrectMultiplication;

        SetupGate(leftGate, correctOnLeft ? correctValue : incorrectValue, correctOnLeft ? correctOp : incorrectOp);
        SetupGate(rightGate, correctOnLeft ? incorrectValue : correctValue, correctOnLeft ? incorrectOp : correctOp);
    }

    private void SetupGate(Transform gate, int value, char op)
    {
        if (gate == null) return;

        GateValueCallculator script = gate.GetComponent<GateValueCallculator>();
        if (script != null)
        {
            script.number = value;
            script.operation = op;
        }
    }

    private void CalculateGateValues()
    {
        int random;

        do random = Random.Range(1, numberOfEnemies * 2);
        while (Lives * random < numberOfEnemies);
        correctMultiplication = random;

        do random = Random.Range(1, numberOfEnemies * 2);
        while (Lives + random < numberOfEnemies);
        correctAddition = random;

        do random = Random.Range(1, numberOfEnemies * 2);
        while (Lives * random >= numberOfEnemies);
        incorrectMultiplication = random;

        do random = Random.Range(1, numberOfEnemies * 2);
        while (Lives + random >= numberOfEnemies);
        incorrectAddition = random;
    }

    private void SpawnEnemies()
    {
        isFight = true;
        isEnemySpawnable = false;
        enemyTimer = 0f;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(380f, 400f), 0.5f, Random.Range(-10f, 5f));
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemies.Add(newEnemy);
        }
    }

    public void ApplyGateEffect(char operation, int value)
    {
        int oldLives = Lives;

        switch (operation)
        {
            case '+': Lives += value; break;
            case '*': Lives *= value; break;
        }

        int newUnits = Lives - oldLives;

        for (int i = 0; i < newUnits; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.2f, 0.2f), 0f, Random.Range(-0.2f, 0.2f));
            GameObject clone = Instantiate(playerPrefab, player.transform.position + offset, Quaternion.identity);
            players.Add(clone);
        }
    }

    private void UpdateUI()
    {
        hpText.text = $"HP: {Lives}";
        enemyText.text = $"Enemies: {numberOfEnemies}";
    }

    private void CheckGameOver()
    {
        if (Lives <= 0)
        {
            Time.timeScale = 0;
            Debug.Log("VÉGE");
        }
    }
}
