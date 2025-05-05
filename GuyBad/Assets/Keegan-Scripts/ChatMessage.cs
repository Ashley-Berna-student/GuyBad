using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessage : MonoBehaviour
{
    [SerializeField] Text messageText;

    public void SetText(string str)
    {
        messageText.text = str;
    }
}
