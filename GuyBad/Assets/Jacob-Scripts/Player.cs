using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    [SerializeField] public GameObject playerCapsule;
    public Color playerColor;
    public string playerName;
    private MeshRenderer ma;
    private bool vote = false;
    private GameObject[] players;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (ma == null)
        {
            ma = playerCapsule.GetComponent<MeshRenderer>();
        }
    }
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Initialize();
            playerColor = Random.ColorHSV();
            ma.material.SetColor("_Color", playerColor);
            //players = GameObject.FindGameObjectsWithTag("Player");
        }
        else
        {
            return;
        }
        
    }
} 
