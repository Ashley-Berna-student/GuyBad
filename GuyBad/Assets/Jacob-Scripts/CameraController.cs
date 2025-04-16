using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class CameraController : NetworkBehaviour
{
    public GameObject cameraHolder;

    private void Start()
    {
        if(cameraHolder == null)
        {
            cameraHolder.SetActive(false);
        }
    }
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            if(cameraHolder != null)
            {
                cameraHolder.SetActive(true);
            }
        }
        else
        {
            cameraHolder.SetActive(false);
        }
    }
}
