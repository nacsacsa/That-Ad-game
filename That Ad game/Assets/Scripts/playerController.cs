using UnityEngine;
using UnityEngine.InputSystem;

public class playerController: MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minZ = -11f;
    public float maxZ = 8f;

    void Update()
    {
        float moveInput = 0f;

        if (Keyboard.current.rightArrowKey.isPressed)
            moveInput = 1f;
        else if (Keyboard.current.leftArrowKey.isPressed)
            moveInput = -1f;

        Vector3 newPosition = transform.position + Vector3.forward * moveInput * moveSpeed * Time.deltaTime;

        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        transform.position = newPosition;
    }
}