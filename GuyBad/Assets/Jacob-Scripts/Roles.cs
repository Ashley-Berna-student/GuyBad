using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class Roles : NetworkBehaviour
{
    private GameObject[] newPlayer = GameObject.FindGameObjectsWithTag("Player");
    void Update()
    {

    }
    public void RoleCall()
    {
        if (newPlayer[0].GetComponent<Player>().role.Value == 1)
        {

        }
    }
}
