using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class SendPlayersToGame : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene";
    [SerializeField] private List<Transform> spawnPoints;

    private bool sceneLoadStarted = false;

    private void Start()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
            // Listen to sceneLoaded from UnityEngine.SceneManagement
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void HandleClientConnected(ulong clientId)
    {
        if (!sceneLoadStarted && NetworkManager.Singleton.ConnectedClientsList.Count == 10)
        {
            sceneLoadStarted = true;
            StartCoroutine(WaitAndStartGame());
        }
    }

    private IEnumerator WaitAndStartGame()
    {
        yield return new WaitForSeconds(10f);
        NetworkManager.Singleton.SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
    }

    // Update this to work with Unity's SceneManager
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == gameSceneName)
        {
            SpawnPlayers();
        }
    }

    private void SpawnPlayers()
    {
        var connectedClients = NetworkManager.Singleton;
        // Logic to spawn players
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
