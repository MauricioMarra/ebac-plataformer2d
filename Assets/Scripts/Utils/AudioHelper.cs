using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    [Header("Configuration")]
    
    [Header("Multiple Sounds")]
    public List<AudioSource> audioSourcesList;
    public List<AudioClip> audioClipList;
    
    [Header("Single Sounds")]
    public AudioClip audioClip;
    public AudioSource audioSource;


    public void PlayMultiple()
    {
        AudioManager.instance.PlayRandom(audioClipList, audioSourcesList);
    }

    public void PlaySingle()
    {
        AudioManager.instance.PlaySingle(audioClip, audioSource);
    }
}
