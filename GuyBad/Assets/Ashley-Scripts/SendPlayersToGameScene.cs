using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class SendPlayersToGame : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene";

    private bool sceneLoadStarted = false;

    private void Start()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
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
}
