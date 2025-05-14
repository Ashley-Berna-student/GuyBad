using System.Collections;
using Unity.Netcode;
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

                if (voteNein.Value >= voteint.Value)
                {
                    uiManager.GivePresidentCards();
                }

                time = 10f;
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
