using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiButton : MonoBehaviour
{
    public int emojiSoundIndex;
    public EmojiSoundManager soundManager;

    public void OnEmojiClicked()
    {
        soundManager.PlayEmojiSound(emojiSoundIndex);
    }
}
