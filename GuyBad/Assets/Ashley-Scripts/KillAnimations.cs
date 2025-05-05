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
        if (!inLobby)
        {
            animator.SetBool("IsInGame", true);
        }

        //reactions stuff for testing purposes
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("IsEvilLaughing", true);
            print("evil laughter");
            StartCoroutine(ResetBool("IsEvilLaughing", 1f));
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetBool("IsClapping", true);
            print("clapping");
            StartCoroutine(ResetBool("IsClapping", 1f));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("IsGasping", true);
            print("gasping");
            StartCoroutine(ResetBool("IsGasping", 1f));
        }
    }

    public void HandleCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("BowlingBall"))
        {
            animator.SetBool("BowlingBall", true);
        }
        if (collision.gameObject.CompareTag("RayGun"))
        {
            animator.SetBool("RayGun", true);
        }
    }
    public void HandleCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("BowlingBall"))
        {
            animator.SetBool("BowlingBall", false);
        }
        if (collision.gameObject.CompareTag("RayGun"))
        {
            animator.SetBool("RayGun", false);
        }
    }

    IEnumerator ResetBool(string paramname, float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(paramname, false);
    }
}
