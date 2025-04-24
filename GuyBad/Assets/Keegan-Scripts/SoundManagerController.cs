using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SoundManagerController : NetworkBehaviour
{
    public static SoundManagerController instance;
    public AudioSource audioSource;

    [System.Serializable]
    public class EmojiSound
    {
        public string emojiName;
        public AudioClip clip;
    }

    public List<EmojiSound> sounds = new List<EmojiSound>();

    private Dictionary<string, AudioClip> soundDict;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            soundDict = new Dictionary<string, AudioClip>();
            foreach (var sound in sounds)
            {
                soundDict[sound.emojiName] = sound.clip;
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayEmojiSoundByIndex(int index)
    {
        if (index >= 0 && index < sounds.Count)
        {
            audioSource.PlayOneShot(sounds[index].clip);
        }

        else
        {
            Debug.LogWarning($"Invalid emoji sound index {index}");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestPlayEmojiSoundServerRpc(int index)
    {
        PlayEmojiSoundClientRpc(index);
    }

    [ClientRpc]
    void PlayEmojiSoundClientRpc(int index)
    {
        PlayEmojiSoundByIndex(index);
    }
}
