using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VoteCount : MonoBehaviour
{
   private int voteint = 0;
   private int voteNein = 0;
    public bool isStarted;
    public float time = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    [Rpc(SendTo.Server)]
    public void RpcTest()
    {
        Debug.Log(voteint);
    }
    void Update()
    {
        /*time -= Time.deltaTime;
        if (Mathf.FloorToInt(time) == 30)
        {
            Debug.Log(voteint);
        }*/
        if (isStarted)
        {
           
            time -= Time.deltaTime;
            if(time >= 0)
            { 
                if(time % 2 == 0)
                {
                    RpcTest();
                }
            }
            else
            {
                Debug.Log("60 Seconds is Over");
                Debug.Log(voteint);
                isStarted = false;
            }
        }
    }

    public void GetStart(bool start)
    {
        if (start)
        {
            isStarted = true;
        }
        else
        {
            isStarted = false;
        }
    }
    
    public void GetVote(bool vote)
    {
        if (vote)
        {
            voteint++;
        }
        else
        {
            voteNein++;
        }
    }


}
