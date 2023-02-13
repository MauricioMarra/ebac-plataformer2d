using UnityEngine;
using UnityEngine.Audio;

public class SliderControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string mixerParameterName;

    public void ChangeSliderValue(float value)
    {
        if (audioMixer != null)
            audioMixer.SetFloat(mixerParameterName, value);
    }
}
