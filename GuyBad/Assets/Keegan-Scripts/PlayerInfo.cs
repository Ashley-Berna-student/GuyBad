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

            GameObject ui = Instantiate(playerUIPrefab);

            var playerUI = ui.GetComponent<PlayerUI>();
            if (playerUI != null && PlayerListManager.Instance != null)
            {
                PlayerListManager.Instance.SetListContainer(playerUI.listContainer);
            }
        }

        playerName.OnValueChanged += (oldValue, newValue) =>
        {
            Debug.Log($"[client] PLayer name updated: {newValue}");
        };
    }

    [ServerRpc]
    void SetPlayerNameServerRpc(string name)
    {
        playerName.Value = name;
    }
}
