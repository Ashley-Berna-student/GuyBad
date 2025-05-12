using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class VoteCount : NetworkBehaviour
{
    public NetworkVariable<int> voteint = new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> voteNein = new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    private NetworkVariable<bool> isStarted = new(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField] public float time = 10f;
    [SerializeField] private Text txt;

    private MoveChancellor chooseChancellor; // Reference to other script
    private float countdown;

    public override void OnNetworkSpawn()
    {
        chooseChancellor = FindObjectOfType<MoveChancellor>();

        voteint.OnValueChanged += (prev, curr) =>
            Debug.Log(OwnerClientId + " voteYes: " + voteint.Value);
        voteNein.OnValueChanged += (prev, curr) =>
            Debug.Log(OwnerClientId + " voteNo: " + voteNein.Value);
        isStarted.OnValueChanged += (prev, curr) =>
            Debug.Log(OwnerClientId + " isStarted: " + isStarted.Value);
    }

    void Update()
    {
        // Start timer if chancellor is chosen
        if (!isStarted.Value && chooseChancellor != null && chooseChancellor.chancellorChosen)
        {
            if (!IsOwner) return; // Only the owner can start it
            isStarted.Value = true;
            chooseChancellor.chancellorChosen = false; // Reset flag so this only runs once
            countdown = time;
        }

        if (isStarted.Value)
        {
            countdown -= Time.deltaTime;
            txt.text = countdown.ToString("F1");

            if (countdown <= 0)
            {
                TimerRpc();
                StartCoroutine(DelayedReset());
            }
        }
    }

    public delegate void VotingEnded(bool votePassed);
    public static event VotingEnded OnVotingEnded;

    [Rpc(SendTo.ClientsAndHost)]
    private void TimerRpc()
    {
        bool result = voteNein.Value >= voteint.Value;
        OnVotingEnded?.Invoke(result);

        Debug.Log("Timer Ended");
        Debug.Log("Yes: " + voteint.Value);
        Debug.Log("No: " + voteNein.Value);

        isStarted.Value = false;
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void GetStartRpc(bool start)
    {
        if (!IsOwner) return;
        isStarted.Value = start;
    }

    [Rpc(SendTo.Server)]
    public void GetVoteRpc(bool vote)
    {
        if (!IsOwner) return;

        if (vote) voteint.Value += 1;
        else voteNein.Value += 1;
    }

    private IEnumerator DelayedReset()
    {
        yield return new WaitForSeconds(2f);
        voteint.Value = 0;
        voteNein.Value = 0;
    }
}
