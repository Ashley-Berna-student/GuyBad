using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HostJoinCodeDisplay : NetworkBehaviour
{
    public GameObject canvasObject;
    public Text joinCodeText;

    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            ShowJoinCode();
        }
        else
        {
            canvasObject.SetActive(false);
        }
    }

    private void ShowJoinCode()
    {
        string code = PlayerPrefs.GetString("RelayJoinCode", "N/A");
        joinCodeText.text = "Join Code: " + code;
        canvasObject.SetActive(true);
    }
}
