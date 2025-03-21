using UnityEngine;
using Unity.Netcode;

public class RpcTest : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsServer && IsOwner)
        {
            ServerOnlyRpc(0, NetworkObjectId);
        }
    }

    [ClientRpc]
    void ClientAndHostRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Client recieved the Rpc #{value} on NetworkObject #{sourceNetworkObjectId}");
        if (IsOwner)
        {
            ServerOnlyRpc(value + 1, sourceNetworkObjectId);
        }
    }

    [ServerRpc]
    void ServerOnlyRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Server received the Rpc #{value} on NetworkObject #{sourceNetworkObjectId}");
        ClientAndHostRpc(value, sourceNetworkObjectId);
    }
}
