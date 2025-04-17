using UnityEngine;
using Unity.Netcode;
using TMPro;
using System.Collections.Generic;

public class ChatManager : NetworkBehaviour
{
    public TMP_InputField chatInputField;
    public TextMeshProUGUI chatDisplay;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrWhiteSpace(chatInputField.text))
        {
            string message = chatInputField.text;
            chatInputField.text = "";
            SendMessageToServer(message);
        }
    }

    void SendMessageToServer(string message)
    {
        SendChatMessageServerRpc(message);
    }

    [ServerRpc(RequireOwnership = false)]
    void SendChatMessageServerRpc(string message, ServerRpcParams rpcParams = default)
    {
        ulong senderId = rpcParams.Receive.SenderClientId;
        string fullMessage = $"Player {senderId}: {message}";
        BroadcastMessageClientRpc(fullMessage);
    }

    [ClientRpc]
    void BroadcastMessageClientRpc(string message)
    {
        chatDisplay.text += message + "\n";
    }
}