using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    [Header("Configuration")]
    public List<AudioSource> audioSourcesList;
    public List<AudioClip> audioClipList;

    public void PlayFootsteps()
    {
        AudioManager.instance.PlayRandom(audioClipList, audioSourcesList);
    }
}
