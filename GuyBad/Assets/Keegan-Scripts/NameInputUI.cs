using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameInputUI : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void SaveName()
    {
        string enteredName = nameInput.text;

        if (!string.IsNullOrWhiteSpace(enteredName))
        {
            PlayerName.player_name = enteredName;
            Debug.Log("Player Name set to: " + enteredName);
        }

        else
        {
            PlayerName.player_name = "Unnamed";
            Debug.LogWarning("Name was blank. Defaulting to 'Unnnamed'");
        }
    }
}
