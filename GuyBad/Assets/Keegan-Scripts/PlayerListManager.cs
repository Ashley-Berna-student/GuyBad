using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerListManager : MonoBehaviour
{
    public static PlayerListManager Instance;

    public Transform listContainer;
    public GameObject playerListItemPrefab;
    private Dictionary<ulong, GameObject> playerListItems = new();

    private bool isListReady = false;
    private Queue<ulong> queuedClients = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(WaitForCanvasAndAssignContainer());
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    private void InitializeListContainer()
    {
        StartCoroutine(WaitForCanvasAndAssignContainer());
    }

    void OnClientConnected(ulong clientId)
    {
        if (!isListReady)
        {
            queuedClients.Enqueue(clientId);
        }

        else
        {
            AddPlayerToList(clientId);
        }
    }

    void OnClientDisconnected(ulong clientId)
    {
        if (playerListItems.TryGetValue(clientId, out GameObject item))
        {
            Destroy(item);
            playerListItems.Remove(clientId);
        }
    }

    private void PlayerListChanged(NetworkListEvent<ulong> changeEvent)
    {
        RebuildPlayerList();
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
            return;
        }

        GameObject item = Instantiate(playerListItemPrefab, listContainer);
        var info = item.GetComponent<PlayerListItemUI>();
        info.SetPlayer(clientId);
        playerListItems.Add(clientId, item);


        /*RectTransform rectTransform = listContainer.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }

        else
        {
            Debug.LogError("listContainer doesn't have a RectTransform component");
        }*/

    }

    public void SetListContainer(Transform container)
    {
        listContainer = container;
    }

    public IEnumerator WaitForCanvasAndAssignContainer()
    {

        GameObject canvas = null;

        while (canvas == null)
        {
            canvas = GameObject.FindGameObjectWithTag("PlayerUI");
            yield return null;
        }

        listContainer = canvas.transform.Find("ListOfPlayers (1)/Scroll View/Viewport/Name Content");

        if (listContainer == null)
        {
            Debug.LogError("List container not found");
            yield break;
        }

        Debug.Log("Canvas and list container found!");
        isListReady = true;

        while (queuedClients.Count > 0)
        {
            AddPlayerToList(queuedClients.Dequeue());
        }

        yield return new WaitForSeconds(0.25f);
        RebuildPlayerList();
    }

    public void RebuildPlayerList()
    {
        ClearList();

        PlayerInfo[] allPlayers = FindObjectsOfType<PlayerInfo>();
        foreach (var player in allPlayers)
        {
            if (!string.IsNullOrEmpty(player.playerName.Value.ToString()) && player.isAlive.Value)
            {
                AddPlayerToList(player.OwnerClientId);
            }
        }
    }

    public static void UpdateAllPlayerLists()
    {
        if (Instance != null)
        {
            Instance.RebuildPlayerList();
        }
    }

    public void ClearList()
    {
        foreach (var item in playerListItems.Values)
        {
            Destroy(item);
        }
        playerListItems.Clear();
    }


    [ClientRpc]
    public void RebuildListClientRpc()
    {
        RebuildPlayerList();
    }

    private void OnEnable()
    {
        PlayerListItemUI.OnPlayerSelected += HandlePlayerSelected;
    }

    private void OnDisable()
    {
        PlayerListItemUI.OnPlayerSelected -= HandlePlayerSelected;
    }

    private void HandlePlayerSelected(ulong clientId)
    {
        Debug.Log($"Client {clientId} selected from UI");

        //Add logic for what happens here

    }

    public void RemovePlayerFromList(ulong clientId)
    {
        if (playerListItems.TryGetValue(clientId, out GameObject item))
        {
            Destroy(item);
            playerListItems.Remove(clientId);
        }
    }
}
