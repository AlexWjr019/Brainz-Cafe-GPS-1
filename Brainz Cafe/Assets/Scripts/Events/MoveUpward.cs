using UnityEngine;

public class MoveUpward : MonoBehaviour
{
    public float speed = 5f;
    public float targetY = 7f;

    void Update()
    {
        // Check if the current y-position is less than the target y-position
        if (transform.position.y < targetY)
        {
            // Move the object upwards
            Vector3 newPos = transform.position;
            newPos.y += speed * Time.deltaTime;
            transform.position = newPos;
        }
    }
}
