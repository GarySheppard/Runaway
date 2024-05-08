using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public void Start()
    {
        float vol = 0;
            if (PlayerPrefs.HasKey("Volume"))
            {
                audioMixer.GetFloat("volume", out vol);
                PlayerPrefs.SetFloat("Volume", vol);
                //Debug.Log("Pref Volume" + PlayerPrefs.GetFloat("Volume"));
                audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("Volume"));
                volSlider.value = PlayerPrefs.GetFloat("Volume");
            }
        float music = 0;
            if (PlayerPrefs.HasKey("Music"))
            {
                audioMixer.GetFloat("music", out music);
                PlayerPrefs.SetFloat("Music", music);
                //Debug.Log("Pref Music" + PlayerPrefs.GetFloat("Music"));
                audioMixer.SetFloat("music", PlayerPrefs.GetFloat("Music"));
                musicSlider.value = PlayerPrefs.GetFloat("Music");
            }
        float sfx = 0;
            if (PlayerPrefs.HasKey("SFX"))
            {
                audioMixer.GetFloat("SFX", out sfx);
                PlayerPrefs.SetFloat("SFX", sfx);
                //Debug.Log("Pref SFX" + PlayerPrefs.GetFloat("SFX"));
                audioMixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX"));
                volSlider.value = PlayerPrefs.GetFloat("SFX");
            }
    
    }
    public void PlayGame()
    {
        //AudioManager.instance.PlaySound("melee_bat");
        SceneManager.LoadScene("Level 1");
    }


    public void ExitGame()
    {
        //AudioManager.instance.PlaySound("melee_bat");
        Debug.Log("EXIT");
        Application.Quit();
    }


}
