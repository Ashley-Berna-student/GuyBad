using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;
using Unity.Services.Authentication;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    public enum roles
    {
        None, 
        Liberal, 
        BadGuy,
        GuyBad
    }
    [SerializeField] public GameObject playerCapsule;
    public Color playerColor = default;
    [SerializeField] public NetworkVariable<roles> role = new NetworkVariable<roles>(roles.None, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public string playerName;
    [SerializeField] private Text roleText;
    private NetworkVariable<MeshRenderer> ma = new NetworkVariable<MeshRenderer>();
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
            ma.Value = playerCapsule.GetComponent<MeshRenderer>();
        }
    }
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            SetColorRpc();
        }
        else
        {
            return;
        }
        
    }
    [Rpc(SendTo.ClientsAndHost)]
    public void SetColorRpc()
    {
        Initialize();
        playerColor = Random.ColorHSV();
        ma.Value.material.SetColor("_Color", playerColor);
        //players = GameObject.FindGameObjectsWithTag("Player");
    }

} 
