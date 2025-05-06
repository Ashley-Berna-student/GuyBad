using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableAndEnableScripts : MonoBehaviour
{
    private PlayerMovement player;
    private LobbyUIHandler uiHandler;

    void Awake()
    {
        player = GetComponent<PlayerMovement>();
        uiHandler = GetComponent<LobbyUIHandler>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Lobby")
        {
            if (player != null)
            {
                player.enabled = true;
            }
            if (uiHandler != null)
            {
                uiHandler.enabled = true;
            }
        }
        else
        {
            if (player != null)
            {
                player.enabled = false;
            }
            if (uiHandler != null)
            {
                uiHandler.enabled = false;
            }
        }
    }
}
