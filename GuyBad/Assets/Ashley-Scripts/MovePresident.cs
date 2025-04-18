using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePresident : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public Transform[] pos;
    public bool moving = false;

    private int currentTargetIndex = 0;

    void Update()
    {
        if (moving && currentTargetIndex < pos.Length)
        {
            Vector3 target = pos[currentTargetIndex].position;
            rb.MovePosition(Vector3.MoveTowards(rb.position, target, speed * Time.deltaTime));

            float targetZ = pos[currentTargetIndex].rotation.eulerAngles.z;
            Quaternion targetRotation = Quaternion.Euler(-90f, 0f, targetZ);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, 400f * Time.deltaTime));
            print("the rotation is " + targetZ);

            if (Vector3.Distance(rb.position, target) < 0.1f)
            {
                moving = false;
                currentTargetIndex++;
                print("is updating");
            }
        }
    }

    public void Movement()
    {
        if (currentTargetIndex < pos.Length)
        {
            moving = true;
            print($"moving to position {currentTargetIndex}");
        }
        else
        {
            currentTargetIndex = 0;
            moving = true;
            print($"restarting and moving to position {currentTargetIndex}");
        }
    }
}
