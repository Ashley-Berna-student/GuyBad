using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerController : MonoBehaviour
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

    public void PlayEmojiSound(string emojiName)
    {
        if (soundDict.TryGetValue(emojiName, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip);
        }

        else
        {
            Debug.LogWarning($"Sound for emoji '{emojiName}' not found!");
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
}
