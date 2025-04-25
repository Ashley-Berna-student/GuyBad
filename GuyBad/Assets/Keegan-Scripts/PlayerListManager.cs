using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class PlayerListManager : MonoBehaviour
{
    public static PlayerListManager Instance { get; private set; }

    public Transform listContainer;
    public GameObject playerListItemPrefab;

    private Dictionary<ulong, GameObject> playerListItems = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetListContainer(Transform container)
    {
        listContainer = container;

        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (!playerListItems.ContainsKey(client.ClientId))
            {
                AddPlayerToList(client.ClientId);
            }
        }
    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;

        if (NetworkManager.Singleton.IsServer)
        {
            foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
            {
                AddPlayerToList(client.ClientId);
            }
        }  
    }

    void OnClientConnected(ulong clientId)
    {
        AddPlayerToList(clientId);
    }

    void OnClientDisconnected(ulong clientId)
    {
        if (playerListItems.TryGetValue(clientId, out GameObject item))
        {
            Destroy(item);
            playerListItems.Remove(clientId);
        }
    }

    void AddPlayerToList(ulong clientId)
    {
        if (playerListItems.ContainsKey(clientId))
        {
            return;
        }

        GameObject item = Instantiate(playerListItemPrefab, listContainer);
        var info = item.GetComponent<PlayerListItemUI>();
        info.SetPlayer(clientId);
        playerListItems.Add(clientId, item);
    }
}
