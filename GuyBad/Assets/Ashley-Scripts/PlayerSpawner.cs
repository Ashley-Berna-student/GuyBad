using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints; // List of spawn points in the GameScene

    private void Start()
    {
        // Ensure spawn points are populated
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        // Start spawning players
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        // Loop through all connected clients and assign them spawn points
        var connectedClients = NetworkManager.Singleton.ConnectedClientsList;
        for (int i = 0; i < connectedClients.Count; i++)
        {
            ulong clientId = connectedClients[i].ClientId;

            // Make sure we don't go out of bounds of the spawn points list
            if (i < spawnPoints.Count)
            {
                Transform spawnPoint = spawnPoints[i];
                SpawnPlayerAtPoint(clientId, spawnPoint);
            }
            else
            {
                Debug.LogWarning("Not enough spawn points for all players. Some players may not have a spawn point.");
                break;
            }
        }
    }

    private void SpawnPlayerAtPoint(ulong clientId, Transform spawnPoint)
    {
        // Ensure each client has a PlayerObject that can be moved to the spawn point
        var playerObject = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(clientId);

        if (playerObject != null)
        {
            playerObject.transform.position = spawnPoint.position; // Move player to spawn point
            playerObject.transform.rotation = spawnPoint.rotation; // Set rotation as well, if needed
        }
        else
        {
            Debug.LogError($"No player object found for client {clientId}. Could not spawn.");
        }
    }
}
