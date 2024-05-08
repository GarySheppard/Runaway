using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }
    public void SetMusic(float music)
    {
        audioMixer.SetFloat("music", music);
        PlayerPrefs.SetFloat("Music", music);
    }
    public void SetSFX(float sfx)
    {
        audioMixer.SetFloat("SFX", sfx);
        PlayerPrefs.SetFloat("SFX", sfx);
    }
}
