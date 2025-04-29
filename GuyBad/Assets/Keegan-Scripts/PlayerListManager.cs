using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;

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

    private void InitializeListContainer()
    {
        GameObject canvas = GameObject.Find("Testing canvas(Clone)");
        if (canvas != null)
        {
            listContainer = canvas.transform.Find("ListOfPlayers (1)/Scroll View/Viewport/Name Content");
            if (listContainer == null)
            {
                Debug.LogError("List container not found!");
            }

            else
            {
                Debug.Log("Successfully found List container: " + listContainer.name);
            }
        }

        else
        {
            Debug.LogError("Canvas not Found!");
        }
    }

    void OnClientConnected(ulong clientId)
    {
        if (listContainer == null)
        {
            InitializeListContainer();
        }

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

        if (listContainer == null)
        {
            Debug.LogError("list container is null. Cannot add player");
        }

        GameObject item = Instantiate(playerListItemPrefab, listContainer);
        var info = item.GetComponent<PlayerListItemUI>();
        info.SetPlayer(clientId);
        playerListItems.Add(clientId, item);


        RectTransform rectTransform = listContainer.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }

        else
        {
            Debug.LogError("listContainer doesn't have a RectTransform component");
        }
        
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
}
