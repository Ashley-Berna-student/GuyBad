using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAutoLoader : NetworkBehaviour
{
    public string lobbySceneName = "Lobby";

    public override void OnNetworkSpawn()
    {
        // Only run on the host
        if (IsServer)
        {
            // Load Lobby scene for everyone
            NetworkManager.Singleton.SceneManager.LoadScene(lobbySceneName, LoadSceneMode.Single);
        }
    }
}
