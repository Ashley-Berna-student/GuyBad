using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class PlayerInfo : NetworkBehaviour
{
    public NetworkVariable<bool> isAlive = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<FixedString64Bytes> playerName = new NetworkVariable<FixedString64Bytes>(writePerm: NetworkVariableWritePermission.Server);
    public GameObject playerUIPrefab;
    public TextMesh nameLabel;

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
            }
        }

        UpdateFloatingName(playerName.Value.ToString());
        playerName.OnValueChanged += OnPlayerNameChanged;

        isAlive.OnValueChanged += OnAliveStateChanged;

        StartCoroutine(NotifyClientsDelayed());
    }

    [ServerRpc]
    void SetPlayerNameServerRpc(string name)
    {
        playerName.Value = name;
    }

    IEnumerator NotifyClientsDelayed()
    {
        // Slight delay to ensure playerName is set before UI rebuild
        yield return new WaitForSeconds(0.5f);

        if (PlayerListManager.Instance != null)
        {
            PlayerListManager.Instance.RebuildListClientRpc();
        }
    }

    private void OnPlayerNameChanged(FixedString64Bytes oldVal, FixedString64Bytes newVal)
    {
        UpdateFloatingName(newVal.ToString());
    }

    private void UpdateFloatingName(string name)
    {
        if (nameLabel != null)
        {
            nameLabel.text = name;
        }
    }

    private void OnDestroy()
    {
        playerName.OnValueChanged -= OnPlayerNameChanged;
    }

    public void killPlayer()
    {
        if (IsServer)
        {
            isAlive.Value = false;
        }
    }

    private void OnAliveStateChanged(bool oldVal, bool newVal)
    {
        if (!newVal)
        {
            PlayerListManager.Instance?.RemovePlayerFromList(OwnerClientId);
        }
    }
}