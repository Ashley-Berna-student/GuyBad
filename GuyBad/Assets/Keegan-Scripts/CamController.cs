using UnityEngine;
using Unity.Netcode;

public class CamController : NetworkBehaviour
{
    [SerializeField] private GameObject playerCam;

    private void Start()
    {
        if (playerCam == null)
        {
            playerCam.gameObject.SetActive(false);
        }
    }


    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            playerCam.gameObject.SetActive(true);
        }

        else
        {
            playerCam.gameObject.SetActive(false);
        }
    }
}
