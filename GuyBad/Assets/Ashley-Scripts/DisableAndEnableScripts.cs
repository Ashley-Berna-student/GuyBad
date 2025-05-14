using System.Collections;
using System.Collections.Generic;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisableAndEnableScripts : MonoBehaviour
{
    private PlayerMovement player;
    private LobbyUIHandler uiHandler;
    private string currentScene = "";

    //jacobs scripts
    private Player jPlayer;
    private ClientNetworkTransform jClientTransform;
    private CameraController jCameraController;
    private PresidentSelectg jPresidentSelect;

    void Awake()
    {
        player = GetComponent<PlayerMovement>();
        uiHandler = GetComponent<LobbyUIHandler>();

        jPlayer = GetComponent<Player>();
        jClientTransform = GetComponent<ClientNetworkTransform>();
        jCameraController = GetComponent<CameraController>();
        jPresidentSelect = GetComponent<PresidentSelectg>();
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

            if (jPlayer != null) jPlayer.enabled = false;
            if (jClientTransform != null) jClientTransform.enabled = false;
            if (jCameraController != null) jCameraController.enabled = false;
            if (jPresidentSelect != null) jPresidentSelect.enabled = false;

            if (GetComponent<Rigidbody>() == null)
            {
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                Debug.Log("Rigidbody added in Lobby scene");
            }
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


            if (jPlayer != null) jPlayer.enabled = true;
            if (jClientTransform != null) jClientTransform.enabled = true;
            if (jCameraController != null) jCameraController.enabled = true;
            if (jPresidentSelect != null) jPresidentSelect.enabled = true;
        }
    }
}