using UnityEngine;
using Unity.Netcode;

public class CamController : NetworkBehaviour
{
    [SerializeField] private Camera playerCam;


    private void Awake()
    {
        playerCam = GetComponentInChildren<Camera>(true);

        Debug.Log($"[{gameObject.name}] Camera found: {playerCam != null}");

        if (playerCam == null)
        {
            Debug.LogError("No camera found in player prefab", this);
            return;
        }
    }

    private void Start()
    {
        if (playerCam == null) return;

        if (!IsOwner)
        {
            playerCam.gameObject.SetActive(false);
        }

        else
        {
            playerCam.gameObject.SetActive(true);
        }
    }
}
