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
            print("You are in the lobby");
            if (player != null)
            {
                player.enabled = true;
                print("player is enabled");
            }
            if (uiHandler != null)
            {
                uiHandler.enabled = true;
                print("handler is enabled");
            }
        }
        else
        {
            print("UIHandler GameObject: " + uiHandler.gameObject.name);
            print("You are not in the lobby");
            if (player != null)
            {
                player.enabled = false;
                print("player is disabled");
            }
            if (uiHandler != null)
            {
                uiHandler.enabled = false;
                print("handler is disabled");
            }
        }
    }
}
