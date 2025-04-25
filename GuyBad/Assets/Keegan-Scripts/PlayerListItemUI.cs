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
        var player = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        var info = player.GetComponent<PlayerInfo>();

        nameText.text = info.playerName.Value.ToString();
        info.playerName.OnValueChanged += (oldName, newName) => nameText.text = newName.ToString();
    }
}
