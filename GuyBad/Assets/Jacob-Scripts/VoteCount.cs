using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Networking;
using UnityEngine;
using UnityEngine.UI;

public class VoteCount : NetworkBehaviour
{
    private NetworkVariable<int> voteint = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<int> voteNein = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<bool> isStarted = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    [SerializeField] public float time = 5f;
    // Start is called before the first frame update
    // Update is called once per frame
    public override void OnNetworkSpawn()
    {
        
    }
    [Rpc(SendTo.ClientsAndHost)]
    public void TestRpc()
    { 
        Debug.Log(voteint.Value);
    }
    void Update()
    {
        if (time == 5f)
        {
            Debug.Log(OwnerClientId + " " + isStarted.Value);
        }
        if (isStarted.Value)
        {
            TestRpc();

            time -= Time.deltaTime;
            if(time >= 0)
            { 
                if(time % 2 == 0)
                {
                    TestRpc();
                }
            }
            else
            {
                Debug.Log("60 Seconds is Over");
                Debug.Log(voteint);
                isStarted.Value = false;
            }
        }
    }

    public void GetStart(bool start)
    {
        if (start)
        {
           isStarted.Value = true;

        }
        else
        {
            isStarted.Value = false;
        }
    }
    
    public void GetVote(bool vote)
    {
        if (!IsOwner) return;
        if (vote)
        {
            voteint.Value++;
        }
        else
        {
            voteNein.Value++;
        }
    }
    [Rpc(SendTo.Server)]
    public void PingRpc(int pingCount)
    {
        // Server -> Clients because PongRpc sends to NotServer
        // Note: This will send to all clients.
        // Sending to the specific client that requested the pong will be discussed in the next section.
        PongRpc(pingCount, "PONG!");
    }

    [Rpc(SendTo.NotServer)]
    void PongRpc(int pingCount, string message)
    {
        Debug.Log($"Received pong from server for ping {pingCount} and message {message}");
    }



}
