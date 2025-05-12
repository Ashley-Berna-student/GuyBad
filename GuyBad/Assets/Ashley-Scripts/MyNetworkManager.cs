using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System.Collections;

public class MyNetworkManager1 : MonoBehaviour
{
    [SerializeField] private string lobbySceneName = "LobbyScene"; // Replace with your actual lobby scene name

    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += HandleServerStarted;
    }

    private void HandleServerStarted()
    {
        StartCoroutine(MoveToLobbyAfterDelay());
    }

    private IEnumerator MoveToLobbyAfterDelay()
    {
        yield return new WaitForSeconds(.1f); // Wait for 2 seconds before switching

        if (NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(lobbySceneName, LoadSceneMode.Single);
        }
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnServerStarted -= HandleServerStarted;
        }
    }
}
