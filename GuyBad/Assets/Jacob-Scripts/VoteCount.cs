using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoteCount : MonoBehaviour
{
    int voteint = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float time = 0f;
        time += Time.deltaTime;
        while (Time.deltaTime <= 60)
        {
            if (vote)
            {
                voteint++;
                Debug.Log(voteint);
            }
        }
        Debug.Log("60 Seconds is Over");
        Debug.Log(voteint);
    }

    public void GetVotes(bool vote)
    {
        
    }
}
