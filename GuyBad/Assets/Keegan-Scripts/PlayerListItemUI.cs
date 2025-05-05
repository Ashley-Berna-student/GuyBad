using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using UnityEngine.UI;
using System;

public class PlayerListItemUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Button selectButton;

    private ulong clientId;

    public static event Action<ulong> OnPlayerSelected;
    
    public void SetPlayer(ulong clientId)
    {
        this.clientId = clientId;
        StartCoroutine(WaitForPlayerName(clientId));

        if (selectButton != null)
        {
            selectButton.onClick.AddListener(OnSelected);
        }
    }

    private void OnSelected()
    {
        Debug.Log($"Player with clientId {clientId} selected");
    }

    private IEnumerator WaitForPlayerName(ulong clientId)
    {
        PlayerInfo info = null;
        float timeout = 2f;
        float elapsed = 0f;

        // Try to find the PlayerInfo belonging to this clientId
        while (info == null && elapsed < timeout)
        {
            foreach (var player in FindObjectsOfType<PlayerInfo>())
            {
                if (player.OwnerClientId == clientId)
                {
                    info = player;
                    break;
                }
            }

            if (info == null)
            {
                elapsed += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
        }

        if (info == null)
        {
            Debug.LogWarning("Could not find PlayerInfo for clientId: " + clientId);
            nameText.text = "Unknown";
            yield break;
        }

        // Wait until the name is set
        while (string.IsNullOrEmpty(info.playerName.Value.ToString()))
        {
            yield return null;
        }

        nameText.text = info.playerName.Value.ToString();

        info.playerName.OnValueChanged += (oldVal, newVal) =>
        {
            nameText.text = newVal.ToString();
        };
    }
}
