using UnityEngine;

public class FollowLeader : MonoBehaviour
{
    private Transform target;
    public Vector3 offset;
    public float moveSpeed = 5f;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        Collider myCol = GetComponent<Collider>();
        Collider targetCol = target.GetComponent<Collider>();
        if (myCol != null && targetCol != null)
        {
            Physics.IgnoreCollision(myCol, targetCol);
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);

        //transform.LookAt(target);
    }
}
