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

            if (PlayerListManager.Instance != null)
            {
                GameObject ui = Instantiate(playerUIPrefab);

                var listManager = ui.GetComponentInChildren<PlayerListManager>();
                if (listManager != null && PlayerListManager.Instance == null)
                {
                    listManager.StartCoroutine(listManager.WaitForCanvasAndAssignContainer());
                }

                else
                {
                    Debug.LogError("PlayerListManager not found in instantiated UI prefab.");
                }
            }
        }

        if (IsServer)
        {
            StartCoroutine(RebuildListAfterDelay());
        }

        /*playerName.OnValueChanged += (oldValue, newValue) =>
        {
            if (PlayerListManager.Instance != null)
            {
                PlayerListManager.Instance.RebuildPlayerList();
            }
        };*/
    }

    [ServerRpc]
    void SetPlayerNameServerRpc(string name)
    {
        playerName.Value = name;
    }

    IEnumerator RebuildListAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerListManager.Instance?.RebuildPlayerList();
    }
}
