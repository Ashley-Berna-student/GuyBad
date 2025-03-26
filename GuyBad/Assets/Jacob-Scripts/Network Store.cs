using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkStore : MonoBehaviour
{
    public NetworkVariable<int> age = new NetworkVariable<int>(
        );
}
