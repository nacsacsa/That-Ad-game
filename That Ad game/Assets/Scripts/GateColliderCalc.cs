using UnityEngine;

public class GateColliderCalc : MonoBehaviour
{
    private GateValueCallculator gate;

    void Start()
    {
        gate = GetComponent<GateValueCallculator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Logic logic = FindAnyObjectByType<Logic>();
            if (logic != null && gate != null)
            {
                logic.ApplyGateEffect(gate.operation, gate.number);
                Destroy(this);
            }
            Destroy(this);
        }
    }
}
