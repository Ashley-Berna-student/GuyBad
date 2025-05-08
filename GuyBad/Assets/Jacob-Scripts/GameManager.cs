using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using static Player;

public class GameManager : NetworkBehaviour
{

    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            AssignRole();
        }
        else
        {
            return;
        }
    }
    public void AssignRole()
    {
        List<Player> playersY = new List<Player>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int liberals = 0;
        int badGuys = 0;
        int GuyBad = 0;
        int i = players.Length;
        foreach (GameObject player in players)
        {
            playersY.Add(player.GetComponent<Player>());
        }
        for (int c = 0; c <= playersY.Count() - 1; c++)
        {
            Player temp = playersY[Random.Range(0, playersY.Count())];
            int r = Random.Range(0, 3);
            if (r == 0 && liberals < 3)
            {
                temp.role.Value = roles.Liberal;
            }
            else if (r == 1 && badGuys < 2)
            {
                temp.role.Value = roles.BadGuy;
            }
            else if (r == 2 && GuyBad != 1)
            {
                temp.role.Value = roles.GuyBad;
            }
            Debug.Log(temp.role.Value.ToString());
        }
    }
    public void AssignPresident()
    {

    }
}

