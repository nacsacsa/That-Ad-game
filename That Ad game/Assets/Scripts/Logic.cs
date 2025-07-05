using UnityEngine;

public class Logic : MonoBehaviour
{
    public int Lives = 1;
    public GameObject player;
    public GameObject obsticlesPrefab;
    public GameObject playerPrefab;

    private float timer = 0f;
    public float spawnInterval = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    private void SpawnObstacle()
    {
        Vector3 spawnPosition = new Vector3(400f, 0.5f, -4.89094f);
        Instantiate(obsticlesPrefab, spawnPosition, Quaternion.identity);
    }

    private bool GameOver()
    {
        return Lives <= 0;
    }
}
