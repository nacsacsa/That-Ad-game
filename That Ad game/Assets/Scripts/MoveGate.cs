using UnityEngine;

public class MoveGate : MonoBehaviour
{
    //public GameObject prefab;          // A prefab, amit mozgatunk
    public float speed = 50f;          // Mozgási sebesség
    //public float spawnX = 300f;        // Spawn pozíció X-ben
    //public float y = 0f;               // Fix Y pozíció
    //public float z = 0f;               // Fix Z pozíció
    //public float respawnDelay = 5f;    // Újragenerálás idõ (mp)

    //private GameObject currentObject;  // Az aktuális példány
    //private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
