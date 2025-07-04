using UnityEngine;

public class FollowLeader : MonoBehaviour
{
    public GameObject target;
    public float followSpeed = 5f;
    public float followDistance = 1f;

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance > followDistance)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;
        }
    }
}
