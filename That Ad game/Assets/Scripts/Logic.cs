using UnityEngine;

public class Logic : MonoBehaviour
{
    public int Lives = 1;
    public GameObject players;
    public GameObject obsticles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool GameOver()
    {
        return Lives <= 0;
    }
}
