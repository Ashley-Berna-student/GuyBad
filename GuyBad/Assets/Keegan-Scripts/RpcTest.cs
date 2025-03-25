using UnityEngine;
using Unity.Netcode;

public class RpcTest : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsServer && IsOwner)
        {
            ServerOnlyServerRpc(0, NetworkObjectId);
        }
    }

    [ClientRpc]
    void ClientAndHostClientRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Client recieved the Rpc #{value} on NetworkObject #{sourceNetworkObjectId}");
        if (IsOwner)
        {
            ServerOnlyServerRpc(value + 1, sourceNetworkObjectId);
        }
    }

    [ServerRpc]
    void ServerOnlyServerRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Server received the Rpc #{value} on NetworkObject #{sourceNetworkObjectId}");
        ClientAndHostClientRpc(value, sourceNetworkObjectId);
    }
}
