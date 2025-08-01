using System.Collections.Generic;
using UnityEngine;

public class MoveToKiill : MonoBehaviour
{
    public float speed = 30f;

    private Transform targetPlayer;
    private Logic logic;
    private static Dictionary<Transform, MoveToKiill> assignedPlayers = new Dictionary<Transform, MoveToKiill>();

    void Start()
    {
        // Megkeresi a Logic scriptet
        GameObject logicObj = GameObject.FindWithTag("logic");
        if (logicObj != null)
        {
            logic = logicObj.GetComponent<Logic>();
        }

        AssignClosestAvailablePlayer();
    }

    void Update()
    {
        // Csak akkor mozdul, ha harcban vagyunk �s van c�lpont
        if (logic != null && logic.isFighting && targetPlayer != null)
        {
            Vector3 direction = (targetPlayer.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else if (targetPlayer == null)
        {
            AssignClosestAvailablePlayer(); // �j j�t�kost keres, ha elvesztette
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

            // Csak olyan player, aki m�g nincs kiosztva
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.transform == targetPlayer)
        {
            GameObject logic = GameObject.FindWithTag("logic");
            if (logic != null)
            {
                Logic logicScript = logic.GetComponent<Logic>();
                logicScript.RemovePlayer(collision.gameObject);
            }

            assignedPlayers.Remove(targetPlayer);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // Tiszt�tja a hozz�rendel�st, ha az enemy meghal
        if (targetPlayer != null && assignedPlayers.ContainsKey(targetPlayer) && assignedPlayers[targetPlayer] == this)
        {
            assignedPlayers.Remove(targetPlayer);
        }
    }
}
