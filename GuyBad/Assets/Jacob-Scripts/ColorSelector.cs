using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ColorSelector : NetworkBehaviour
{
    public NetworkVariable<Color> playerColor = new NetworkVariable<Color>(Color.cyan, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {

        if (IsOwner)
        {
            playerColor.Value = Random.ColorHSV();
        }
        playerColor.OnValueChanged += OnColorChanged;
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        GetComponent<MeshRenderer>().material.color = newColor;
    }
}
