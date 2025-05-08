using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerChatUI : MonoBehaviour
{
    public TMP_InputField chatInput;
    public TextMeshProUGUI chatDisplay;

    void Start()
    {
        ChatManager manager = FindObjectOfType<ChatManager>();
        if (manager != null)
        {
            manager.chatInputField = chatInput;
            manager.chatDisplay = chatDisplay;
        }
    }
}
