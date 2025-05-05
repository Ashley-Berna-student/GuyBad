using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EmojiSoundManager : NetworkBehaviour
{
    public AudioClip[] emojiSounds;
    public AudioSource audioSource;

    public void PlayEmojiSound(int index)
    {
        if (IsOwner || IsHost)
        {
            PlayEmojiSoundClientRpc(index);
        }
    }

    [ClientRpc]
    void PlayEmojiSoundClientRpc(int index)
    {
        if (emojiSounds != null && index >= 0 && index < emojiSounds.Length)
        {
            audioSource.PlayOneShot(emojiSounds[index]);
        }
    }
}
