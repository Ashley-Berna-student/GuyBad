using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableAndEnableScripts : MonoBehaviour
{
    private PlayerMovement player;
    private LobbyUIHandler uiHandler;
    private string currentScene = "";

    void Awake()
    {
        player = GetComponent<PlayerMovement>();
        uiHandler = GetComponent<LobbyUIHandler>();
    }

    void Update()
    {
        string activeScene = SceneManager.GetActiveScene().name;

        if (activeScene != currentScene)
        {
            currentScene = activeScene;
            HandleSceneChange(currentScene);
        }
    }

    private void HandleSceneChange(string sceneName)
    {
        if (sceneName == "Lobby")
        {
            if (player != null) player.enabled = true;
            if (uiHandler != null) uiHandler.enabled = true;
        }
        else
        {
            if (player != null) player.enabled = false;
            if (uiHandler != null) uiHandler.enabled = false;
        }

        if (sceneName == "GameScene")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null) Destroy(rb);
        }
    }
}