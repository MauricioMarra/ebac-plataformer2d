using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private int _indexPlayRandom = 0;
    public void PlayRandom(List<AudioClip> audioClipList, List<AudioSource> audioSourcesList)
    {
        if (_indexPlayRandom >= audioSourcesList.Count)
            _indexPlayRandom = 0;

        audioSourcesList[_indexPlayRandom].clip = audioClipList[Random.Range(0, audioClipList.Count)];
        audioSourcesList[_indexPlayRandom].Play();

        _indexPlayRandom++;
    }
}
