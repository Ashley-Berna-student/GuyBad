using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    [SerializeField] public GameObject playerCapsule;
    public Color playerColor;
    public NetworkVariable<int> role = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public string playerName;
    private MeshRenderer ma = new MeshRenderer();
    private List<MeshRenderer> renderers = new List<MeshRenderer>();
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
            role.Value = Random.Range(0, 3);
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
