using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiButton : MonoBehaviour
{
    public int emojiSoundIndex;
    public SoundManagerController soundManager;

    private void Start()
    {
        if (soundManager == null)
        {
            soundManager = SoundManagerController.instance;
        }
    }

    public void OnEmojiClicked()
    {
        if (soundManager != null)
        {
            soundManager.RequestPlayEmojiSoundServerRpc(emojiSoundIndex);
        }

        else
        {
            Debug.LogWarning("Sound Manager is not assigned on EmojiButton.");
        }
    }
}
