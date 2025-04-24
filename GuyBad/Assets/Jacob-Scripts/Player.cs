using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;
using Unity.Services.Authentication;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField] public GameObject playerCapsule;
    public Color playerColor;
    [SerializeField] public NetworkVariable<string> role = new NetworkVariable<string>(null, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public string playerName;
    [SerializeField] private Text roleText;
    private MeshRenderer ma = new MeshRenderer();
    private List<MeshRenderer> renderers = new List<MeshRenderer>();

    private void Start()
    {
        Initialize();
        //roleText.text = role.Value;
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
