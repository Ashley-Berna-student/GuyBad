using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class PlayerInfo : NetworkBehaviour
{
    public NetworkVariable<FixedString64Bytes> playerName = new NetworkVariable<FixedString64Bytes>(writePerm: NetworkVariableWritePermission.Server);
    public GameObject playerUIPrefab;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            string chosenName = PlayerName.player_name;
            SetPlayerNameServerRpc(chosenName);
        }

        GameObject ui = Instantiate(playerUIPrefab);

        playerName.OnValueChanged += (oldValue, newValue) =>
        {
            Debug.Log($"[client] Player name updated: {newValue}");
        };

        if (PlayerListManager.Instance != null)
        {
            PlayerListManager.Instance.SetListContainer(ui.transform.Find("ListOfPlayers (1)/Scroll View/ Viewport/Name Content"));
        }
    }

    [ServerRpc]
    void SetPlayerNameServerRpc(string name)
    {
        playerName.Value = name;
    }
}
