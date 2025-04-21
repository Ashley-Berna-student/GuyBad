using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAnimations : MonoBehaviour
{
    private Animator animator;
    public GameObject bowlingBall;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            animator.SetBool("IsInGame", true);
            print("bool is true");
        }

/*        if (gameObject.collidesWith("BowlingBall"))
        {
            animator.SetBool("BowlingBall", true);
            print("bowling ball is true");
        }
        else
        {
            animator.SetBool("BowlingBall", false);
        }*/

        if (Input.GetKey(KeyCode.R))
        {
            animator.SetBool("RayGun", true);
            print("ray gun is true");
        }
        else
        {
            animator.SetBool("RayGun", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        bowlingBall = collision.gameObject;
        print($"collision with: {collision.gameObject.name}");
    }
}
