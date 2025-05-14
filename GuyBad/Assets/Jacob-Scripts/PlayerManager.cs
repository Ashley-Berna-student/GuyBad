using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using static Player;

public class PlayerManager : NetworkBehaviour
{
    [SerializeField] private GameManager gameManager;
    int maxValue;
    public override void OnNetworkSpawn()
    {
    }
    private void Update()
    {
        if (IsServer)
        {
            if (playerQueue.Count == 3)
            {
                AssignRoleRpc();
                AssignRandomPresidentRpc();
            }
        }
    }
    private List<Player> playerQueue = new List<Player>();

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
        playerQueue.Add(playerScript);
        Debug.Log($"Player {playerScript.name} Added.");
    }


    public int GetQueueCount()
    {
        return playerQueue.Count;
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void AssignRoleRpc()
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
            Player temp = playersY[UnityEngine.Random.Range(0, playersY.Count())];
            int r = UnityEngine.Random.Range(0, 3);
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
        bool presidentExist = false;
        if (playerQueue.Count >= 3)
        {
            if (!presidentExist)
            {
                Player[] players = playerQueue.ToArray();
                Player randomPlayer = players[UnityEngine.Random.Range(0, players.Length)];
                randomPlayer.president.Value = true;
                Debug.Log($"Player {randomPlayer.name} set as President.");
                presidentExist = true;
            }
            else
            {
                return;
            }
        }
    }
}
