using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Unity.Netcode;

public class WinScreenController : NetworkBehaviour
{
    public static WinScreenController LocalInstance;

    public GameObject goodWinScreen;
    public GameObject badWinScreen;

    private void Awake()
    {
        if (IsOwner)
        {
            LocalInstance = this;
        }
    }

    public void ShowWinScreen(string team)
    {
        if (team == "Good") 
        {
            goodWinScreen.SetActive(true);
        }

        else if (team == "Bad")
        {
            badWinScreen.SetActive(true);
        }
    }
}
