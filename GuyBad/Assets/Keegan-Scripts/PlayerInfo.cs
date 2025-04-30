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

        var listManager = ui.GetComponentInChildren<PlayerListManager>();
        if (listManager != null)
        {
            listManager.StartCoroutine(listManager.WaitForCanvasAndAssignContainer());
        }

        playerName.OnValueChanged += (oldValue, newValue) =>
        {
            Debug.Log($"[client] Player name updated: {newValue}");
        };
    }

    [ServerRpc]
    void SetPlayerNameServerRpc(string name)
    {
        playerName.Value = name;
    }
}
