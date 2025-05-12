using System.Collections;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using UnityEngine.UI;

public class PlayerInfo : NetworkBehaviour
{
    public NetworkVariable<bool> isAlive = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<FixedString64Bytes> playerName = new NetworkVariable<FixedString64Bytes>(writePerm: NetworkVariableWritePermission.Server);
    public TextMesh nameLabel;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            string chosenName = PlayerName.player_name;
            SetPlayerNameServerRpc(chosenName);
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
