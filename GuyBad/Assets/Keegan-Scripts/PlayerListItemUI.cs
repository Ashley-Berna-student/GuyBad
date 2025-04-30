using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;

public class PlayerListItemUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    
    public void SetPlayer(ulong clientId)
    {
        StartCoroutine(WaitForPlayerName(clientId));
    }

    private IEnumerator WaitForPlayerName(ulong clientId)
    {
        NetworkObject player = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        if (player == null)
        {
            Debug.LogError("NoPlayerObject for clientId: " + clientId);
            yield break;
        }

        PlayerInfo info = player.GetComponent<PlayerInfo>();

        while (string.IsNullOrEmpty(info.playerName.Value.ToString()))
        {
            yield return null;
        }

        nameText.text = info.playerName.Value.ToString();

        info.playerName.OnValueChanged += (oldVal, newVal) =>
        {
            nameText.text = newVal.ToString();
        };
    }
}
