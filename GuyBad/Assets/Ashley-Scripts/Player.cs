using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 100f;
    public Transform cameraTransform;
    private Rigidbody rb;
    public Animator animator;
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;
    public Animator animator4;
    public Animator animator5;
    public Animator animator6;
    public Animator animator7;
    public Animator animator8;
    public Animator animator9;
    public Animator animator10;
    public Animator animator11;
    public Animator animator12;
    public Animator animator13;
    public GameObject tealChar;
    public GameObject whiteChar;
    public GameObject hotPinkChar;
    public GameObject darkGreenChar;
    public GameObject orangeChar;
    public GameObject brownChar;
    public GameObject lightGreenChar;
    public GameObject pinkChar;
    public GameObject blueChar;
    public GameObject purpleChar;
    public GameObject yellowChar;
    public GameObject greyChar;
    public GameObject redChar;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                return;
            }
        }

        if (tealChar.activeSelf)
        {
            animator = animator1;
        }
        else if (whiteChar.activeSelf)
        {
            animator = animator2;
        }
        else if (hotPinkChar.activeSelf)
        {
            animator = animator3;
        }
        else if (darkGreenChar.activeSelf)
        {
            animator = animator4;
        }
        else if (orangeChar.activeSelf)
        {
            animator = animator5;
        }
        else if (brownChar.activeSelf)
        {
            animator = animator6;
        }
        else if (lightGreenChar.activeSelf)
        {
            animator = animator7;
        }
        else if (pinkChar.activeSelf)
        {
            animator = animator8;
        }
        else if (blueChar.activeSelf)
        {
            animator = animator9;
        }
        else if (purpleChar.activeSelf)
        {
            animator = animator10;
        }
        else if (yellowChar.activeSelf)
        {
            animator = animator11;
        }
        else if (greyChar.activeSelf)
        {
            animator = animator12;
        }
        else if (redChar.activeSelf)
        {
            animator = animator13;
        }

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
