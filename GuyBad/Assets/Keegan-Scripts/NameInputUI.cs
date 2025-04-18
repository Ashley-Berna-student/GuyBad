using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameInputUI : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void OnNameChanged()
    {
        if (!string.IsNullOrWhiteSpace(nameInput.text))
        {
            PlayerName.player_name = nameInput.text;
        }
    }
}
