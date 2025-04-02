using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 100f;
    public Transform cameraTransform;
    private Rigidbody rb;
    public Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (cameraTransform == null)
        {
            print("Camera transform is not assigned");
            return;
        }

        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 moveDirection = cameraForward * moveInput * speed;
        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);

        if (turnInput != 0)
        {
            transform.Rotate(Vector3.up, turnInput * rotationSpeed * Time.deltaTime);
        }
        animator.SetFloat("Speed", moveInput);
    }
}
