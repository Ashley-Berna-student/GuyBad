using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;

    private void Start()
    {
        if (!NetworkManager.Singleton.IsServer) return;

        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        StartCoroutine(DelayedSpawn());
    }

    private IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(1f); // Give time for clients to finish loading
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        var clients = NetworkManager.Singleton.ConnectedClientsList;

        for (int i = 0; i < clients.Count; i++)
        {
            var clientId = clients[i].ClientId;
            var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;

            if (playerObject != null && i < spawnPoints.Count)
            {
                playerObject.transform.position = spawnPoints[i].position;
                playerObject.transform.rotation = spawnPoints[i].rotation;
                Debug.Log($"Spawned player {clientId} at {spawnPoints[i].position}");
            }
        }
    }
}
