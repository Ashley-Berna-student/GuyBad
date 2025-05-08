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
    [SerializeField] public UIManager uiManager; 
    private  NetworkVariable<bool> isStarted = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    [SerializeField] public float time = 5f;
    [SerializeField] private Text txt;
    // Start is called before the first frame update
    // Update is called once per frame

    //On NetworkSpawn runs when a networkprefab is instantiated into the game
    public override void OnNetworkSpawn()
    {
        voteint.OnValueChanged += (int previousValue, int newValue) =>
        {
            Debug.Log(OwnerClientId + ";  randomNumber: " + voteint.Value);
        };
        voteNein.OnValueChanged += (int previousValue, int newValue) =>
        {
            Debug.Log(OwnerClientId + ";  randomNumber: " + voteNein.Value);
        };
        isStarted.OnValueChanged += (bool previousval, bool newVal) => {

            Debug.Log(OwnerClientId + ";  isStarted: " + isStarted.Value.ToString());
        };
    }
    private void Start()
    {
        uiManager = GetComponent<UIManager>();
    }
    void Update()
    {
        if (isStarted.Value)
        {
            float countdown = time -= Time.deltaTime;
            txt.text = countdown.ToString("F1");
            if(countdown <= 0)
            {
                TimerRpc();
                if (voteNein.Value >= voteint.Value)
                {
                    uiManager.GivePresidentCards();
                }
                time = 10f;
            }
        }
        else
        {
            time = 10f; 
            voteint.Value = 0;
            voteNein.Value = 0;
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void TimerRpc()
    {

        Debug.Log("60 Seconds is Over");
        Debug.Log("Yes: " + voteint.Value);
        Debug.Log("No: " + voteNein.Value);
        Debug.Log(time);

            isStarted.Value = false;
        
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void GetStartRpc(bool start)
    {
        if (!IsOwner) { return; }
        isStarted.Value = start;
    }
    [Rpc(SendTo.Server)]
    public void GetVoteRpc(bool vote)
    {
        if (!IsOwner) { return;}
        if (vote)
        {
            voteint.Value += 1;
        }
        else
        {
            voteNein.Value += 1; // changed from -=
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
