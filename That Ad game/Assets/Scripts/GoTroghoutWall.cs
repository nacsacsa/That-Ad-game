using UnityEngine;

public class GoTroghoutWall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            Debug.Log("Enemy átment a falon: " + collision.gameObject.name);
        }
    }
}
