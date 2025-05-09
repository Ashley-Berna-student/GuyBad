using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using static Player;

public class GameManager : NetworkBehaviour
{
    private Queue<Player> q = new Queue<Player>();

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {

            AssignRole();
        }
        else
        {
            return;
        }
    }
    private void Update()
    {
        if (IsServer)
        {
            AssignRandomPresidentRpc();
        }
    }
    private Queue<Player> playerQueue = new Queue<Player>();

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        Player playerScript = GetPlayer(clientId);
        if (playerScript != null)
        {
            EnqueuePlayer(playerScript);
        }
    }

    private Player GetPlayer(ulong clientId)
    {
        NetworkObject playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        return playerObject != null ? playerObject.GetComponent<Player>() : null;
    }

    private void EnqueuePlayer(Player playerScript)
    {
        playerQueue.Enqueue(playerScript);
        Debug.Log($"Player {playerScript.name} enqueued.");
    }

    public Player DequeuePlayer()
    {
        if (playerQueue.Count > 0)
        {
            Player playerScript = playerQueue.Dequeue();
            Debug.Log($"Player {playerScript.name} dequeued.");
            return playerScript;
        }
        return null;
    }

    public int GetQueueCount()
    {
        return playerQueue.Count;
    }
    public void AssignRole()
    {
        List<Player> playersY = new List<Player>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int liberals = 0;
        int badGuys = 0;
        int GuyBad = 0;
        int i = players.Length;
        foreach (GameObject player in players)
        {
            playersY.Add(player.GetComponent<Player>());
        }
        for (int c = 0; c <= playersY.Count() - 1; c++)
        {
            Player temp = playersY[Random.Range(0, playersY.Count())];
            int r = Random.Range(0, 3);
            if (r == 0 && liberals < 3)
            {
                temp.role.Value = roles.Liberal;
            }
            else if (r == 1 && badGuys < 2)
            {
                temp.role.Value = roles.BadGuy;
            }
            else if (r == 2 && GuyBad != 1)
            {
                temp.role.Value = roles.GuyBad;
            }
            Debug.Log(temp.role.Value.ToString());
        }
    }
    [Rpc(SendTo.ClientsAndHost)]
    public void AssignRandomPresidentRpc()
    {
        if (playerQueue.Count == 5)
        {
            Player[] players = playerQueue.ToArray();
            Player randomPlayer = players[Random.Range(0, players.Length)];
            randomPlayer.president = true;
            Debug.Log($"Player {randomPlayer.name} set as President.");
        }
    }
}

