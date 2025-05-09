using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePresident : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public Transform[] pos;
    public bool moving = false;

    //temporary chippy logic
    public int chippyMoves = 0;
    public Rigidbody chippyrb;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;

    private bool voteIsNein = false;
    private int currentTargetIndex = 0;

    public GameObject[] policyCards;
    private bool[] hasMovedForObject;
    public bool chippyFlippedPolicy = false;
    private bool hasFlippedPolicy = false;

    public VoteCount voteCount;

    void Start()
    {
        hasMovedForObject = new bool[policyCards.Length];
    }

    void OnEnable()
    {
        VoteCount.OnVotingEnded += OnVoteResolved;
    }

    void OnDisable()
    {
        VoteCount.OnVotingEnded -= OnVoteResolved;
    }

    void OnVoteResolved(bool voteFailed)
    {
        if (voteFailed)
        {
            chippyMoves++;
            print("chippy has moved");
        }
        else
        {
            print("chippy will not move");
        }
    }

    void Update()
    {
        for (int i = 0; i < policyCards.Length; i++)
        {
            if (policyCards[i].activeSelf && !hasMovedForObject[i])
            {
                Movement();
                hasMovedForObject[i] = true;
            }
        }

        if (moving && currentTargetIndex < pos.Length)
        {
            Vector3 target = pos[currentTargetIndex].position;
            rb.MovePosition(Vector3.MoveTowards(rb.position, target, speed * Time.deltaTime));

            float targetZ = pos[currentTargetIndex].localEulerAngles.z;
            Quaternion targetRotation = pos[currentTargetIndex].rotation;
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, 400f * Time.deltaTime));

            if (Vector3.Distance(rb.position, target) < 0.1f)
            {
                moving = false;
                currentTargetIndex++;
            }
        }

        if (chippyMoves == 0)
        {
            chippyrb.MovePosition(pos1.position);
        }
        if (chippyMoves == 1)
        {
            chippyrb.MovePosition(pos2.position);
        }
        if (chippyMoves == 2)
        {
            chippyrb.MovePosition(pos3.position);
        }
        if (chippyMoves == 3)
        {
            chippyrb.MovePosition(pos4.position);
            
            if (!hasFlippedPolicy)
            {
                chippyFlippedPolicy = true;
                hasFlippedPolicy = true;
                StartCoroutine(PauseChippy(2f));
            }
        }
    }

    public void Movement()
    {
        if (currentTargetIndex < pos.Length)
        {
            moving = true;
            //print($"moving to position {currentTargetIndex}");
        }
        else
        {
            currentTargetIndex = 0;
            moving = true;
            //print($"restarting and moving to position {currentTargetIndex}");
        }
    }

    IEnumerator PauseChippy(float delay)
    {
        yield return new WaitForSeconds(delay);
        chippyMoves = 0;
        hasFlippedPolicy = false;
    }
}
