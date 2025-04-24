using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class DetermineRole : NetworkBehaviour
{
    public NetworkVariable<List<Player>> networkPlayers = new NetworkVariable<List<Player>>(null, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
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
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in players)
        {
            networkPlayers.Value.Add(item.GetComponent<Player>());
        }
        if (IsOwner)
        {
            int length = networkPlayers.Value.Count();
            Player player = networkPlayers.Value[Random.Range(0, length)];
            int i = Random.Range(0, 3);
            foreach (Player player1 in networkPlayers.Value)
            {
                i = Random.Range(0, 3);
                switch (i)
                {
                    case 0:
                        player1.role.Value = "Liberal";
                        break;
                    case 1:
                        player1.role.Value = "Bad Guy";
                        break;
                    case 2:
                        player1.role.Value = "Guy Bad";
                        break;
                }
            }
        }
    }
}
