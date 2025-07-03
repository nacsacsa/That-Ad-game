using UnityEngine;

public class playerController: MonoBehaviour
{
    public float moveSpeed = 5f;       // Mozgási sebesség
    public float maxX = 100f;          // Maximális távolság jobbra és balra

    void Update()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
            moveInput = 1f;
        else if (Input.GetKey(KeyCode.LeftArrow))
            moveInput = -1f;

        Vector3 newPosition = transform.position + Vector3.right * moveInput * moveSpeed * Time.deltaTime;

        // Korlátozzuk az X pozíciót -maxX és +maxX között
        newPosition.x = Mathf.Clamp(newPosition.x, -maxX, maxX);

        transform.position = newPosition;
    }
}