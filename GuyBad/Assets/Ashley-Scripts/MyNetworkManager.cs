using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class MyNetworkManager1 : MonoBehaviour
{
    [SerializeField] private string lobbySceneName = "LobbyScene"; // Replace with your actual lobby scene name

    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += HandleServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
    }

    private void HandleServerStarted()
    {
        // Move to the lobby scene immediately after the server starts
        MoveToLobby();
    }

    private void HandleClientConnected(ulong clientId)
    {
        // When a client connects, spawn their player
        if (NetworkManager.Singleton.IsServer)
        {
            SpawnPlayerForClient(clientId);
        }
    }

    private void MoveToLobby()
    {
        // If this is the server, load the lobby scene immediately
        if (NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(lobbySceneName, LoadSceneMode.Single);
        }
        else
        {
            // Ensure clients know they should join the same scene
            NetworkManager.Singleton.SceneManager.LoadScene(lobbySceneName, LoadSceneMode.Single);
        }
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnServerStarted -= HandleServerStarted;
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
        }
    }

    // This is called to spawn a player for the connected client
    private void SpawnPlayerForClient(ulong clientId)
    {
        // Check if the client already has a player object
        if (NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject == null)
        {
            // Instantiate the player object
            GameObject playerPrefab = Instantiate(NetworkManager.Singleton.NetworkConfig.PlayerPrefab);
            playerPrefab.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true); // 'true' gives ownership
        }
    }
}
