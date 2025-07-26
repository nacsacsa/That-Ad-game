using System.Collections.Generic;
using UnityEngine;

public class MoveToKiill : MonoBehaviour
{
    public float speed = 3f;

    private Transform targetPlayer;
    private static Dictionary<Transform, MoveToKiill> assignedPlayers = new Dictionary<Transform, MoveToKiill>();

    void Start()
    {
        AssignClosestAvailablePlayer();
    }

    void Update()
    {
        if (targetPlayer != null)
        {
            Vector3 direction = (targetPlayer.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            AssignClosestAvailablePlayer();
        }
    }

    void AssignClosestAvailablePlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        float minDistance = Mathf.Infinity;
        Transform closestPlayer = null;

        foreach (GameObject player in players)
        {
            Transform playerTransform = player.transform;

            if (!assignedPlayers.ContainsKey(playerTransform))
            {
                float distance = Vector3.Distance(transform.position, playerTransform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPlayer = playerTransform;
                }
            }
        }

        if (closestPlayer != null)
        {
            targetPlayer = closestPlayer;
            assignedPlayers[targetPlayer] = this;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform == targetPlayer)
        {
            assignedPlayers.Remove(targetPlayer);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (targetPlayer != null && assignedPlayers.ContainsKey(targetPlayer) && assignedPlayers[targetPlayer] == this)
        {
            assignedPlayers.Remove(targetPlayer);
        }
    }
}
