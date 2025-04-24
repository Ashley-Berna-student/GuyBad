using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public NetworkVariable<List<Player>> networkPlayers = new NetworkVariable<List<Player>>(null, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in players)
        {
            networkPlayers.Value.Add(item.GetComponent<Player>()); 
        }
    }  
}

