using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillAnimations : MonoBehaviour
{
    private Animator animator;
    private bool inLobby = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Lobby")
        {
            inLobby = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inLobby)
        {
            animator.SetBool("IsInGame", true);
            print("inLobby is true");
        }
    }

    public void HandleCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("BowlingBall"))
        {
            animator.SetBool("BowlingBall", true);
            print("bowlingball is true");
        }
        if (collision.gameObject.CompareTag("RayGun"))
        {
            animator.SetBool("RayGun", true);
            print("raygun is true");
        }
    }
    public void HandleCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("BowlingBall"))
        {
            animator.SetBool("BowlingBall", false);
            print("bowlingBAll is false");
        }
        if (collision.gameObject.CompareTag("RayGun"))
        {
            animator.SetBool("RayGun", false);
            print("ray gun is false");
        }
    }
}
