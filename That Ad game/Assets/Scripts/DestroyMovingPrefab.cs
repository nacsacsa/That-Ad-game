using UnityEngine;
using UnityEngine.UIElements;

public class DestroyMovingPrefab : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 520)
        {
            Destroy(this.gameObject);
        }
    }
}
