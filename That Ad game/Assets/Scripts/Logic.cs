using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    public int Lives = 1;
    public GameObject player;
    public GameObject obsticlesPrefab;
    public GameObject playerPrefab;

    private float timer = 0f;
    public float spawnInterval = 5f;
    private List<GameObject> players = new List<GameObject>();

    void Start()
    {
        players.Add(player);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
        if (GameOver())
        {
            Time.timeScale = 0;
            Debug.Log("VÉGE");
        }
        Debug.Log(Lives);
    }

    private void SpawnObstacle()
    {
        Vector3 spawnPosition = new Vector3(400f, 0.5f, -4.89094f);
        Instantiate(obsticlesPrefab, spawnPosition, Quaternion.identity);
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
                float numm1 = Random.Range(-0.3f, 0.3f);
                float numm2 = Random.Range(-0.3f, 0.3f);
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
