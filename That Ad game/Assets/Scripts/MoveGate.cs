using UnityEngine;

public class MoveGate : MonoBehaviour
{
    //public GameObject prefab;          // A prefab, amit mozgatunk
    public float speed = 50f;          // Mozg�si sebess�g
    //public float spawnX = 300f;        // Spawn poz�ci� X-ben
    //public float y = 0f;               // Fix Y poz�ci�
    //public float z = 0f;               // Fix Z poz�ci�
    //public float respawnDelay = 5f;    // �jragener�l�s id� (mp)

    //private GameObject currentObject;  // Az aktu�lis p�ld�ny
    //private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
