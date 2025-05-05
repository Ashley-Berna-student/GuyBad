using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class RoleManager : NetworkBehaviour
{
    public NetworkVariable<int> roleId = new NetworkVariable<int>();

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            roleId.Value = Random.Range(0, 10);
        }
    }
}
