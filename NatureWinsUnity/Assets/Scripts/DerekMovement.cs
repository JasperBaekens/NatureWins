using UnityEngine;

public class DerekMovement : MonoBehaviour
{
    public float DerekMovementSpeed = 0.4f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * DerekMovementSpeed * Time.deltaTime;
    }
}
