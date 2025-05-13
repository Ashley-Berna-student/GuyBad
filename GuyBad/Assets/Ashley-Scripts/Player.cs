using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 100f;
    public Transform cameraTransform;
    public Animator animator;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!IsServer && rb != null)
        {
            rb.isKinematic = true; // Server controls physics
        }
    }

    private void Update()
    {
        // Only process input for the local player (this prevents other players from moving based on input)
        if (!IsOwner) return;

        if (cameraTransform == null)
        {
            Debug.LogWarning("Camera transform not assigned.");
            return;
        }

        // Input from local player
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Calculate direction based on camera
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 move = cameraForward * moveInput * speed * Time.deltaTime;
        float turn = turnInput * rotationSpeed * Time.deltaTime;

        // Immediately move the player for the local player
        transform.position += move;
        transform.Rotate(Vector3.up, turn);

        // Handle local animation
        if (animator != null)
        {
            animator.SetFloat("Speed", moveInput);
        }

        // Send the movement data to the server for syncing other clients
        MoveServerRpc(move, turn);
    }

    [ServerRpc]
    private void MoveServerRpc(Vector3 move, float turn)
    {
        // Sync the position and rotation on the server
        transform.position += move;
        transform.Rotate(Vector3.up, turn);
    }
}
