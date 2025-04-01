using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveSpeed = 10f;
    float xInput;
    float yInput;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        rb.AddForce(xInput * moveSpeed, 0, yInput * moveSpeed);
    }
}
