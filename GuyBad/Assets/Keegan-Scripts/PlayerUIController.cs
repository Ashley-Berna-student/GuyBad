using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerUIController : NetworkBehaviour
{
    public GameObject[] roleUI;
    private RoleManager playerRole;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            playerRole = GetComponent<RoleManager>();
            playerRole.roleId.OnValueChanged += OnRoleChanged;
            OnRoleChanged(0, playerRole.roleId.Value);
        }
    }

    void OnRoleChanged(int oldRole, int newRole)
    {
        for (int i = 0; i < roleUI.Length; i++)
        {
            roleUI[i].SetActive(i == newRole);
        }
    }
}
