using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class DetermineRole : NetworkBehaviour
{
    // Start is called before the first frame update
    /*void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in players)
        {
            networkPlayers.Value.Add(item.GetComponent<Player>());
        }
    }*/
    public override void OnNetworkSpawn()
    {

    }
}
