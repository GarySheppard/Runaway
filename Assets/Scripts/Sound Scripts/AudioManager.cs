using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Singleton instance of the AudioManager
    public static AudioManager instance;    

    // Sound class to hold audio clip and audio source
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public AudioMixerGroup mixerGroup;

        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0.1f, 3f)]
        public float pitch = 1f;

        [HideInInspector]
        public AudioSource source;
    }

    public List<Sound> sounds = new List<Sound>();
    public List<AudioClip> clips = new List<AudioClip>();
    public AudioMixerGroup masterMixerGroup;
    public AudioMixerGroup backgroundMixerGroup;
    public AudioMixerGroup inGameMixerGroup;
    
    void Awake()
    {
        // Singleton pattern
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Create AudioSources for each sound
        foreach (AudioClip clip in clips)
        {
            Sound sound = new Sound();
            sound.name = clip.name; // You might want to name the sound after the clip
            sound.clip = clip;
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            if (clip.name.Contains("background"))
            {
                sound.source.Play();
                sound.mixerGroup = backgroundMixerGroup;
                sound.volume = 0.5f;
            }
            else 
            {
                sound.mixerGroup = inGameMixerGroup;
                if (clip.name.Contains("zombie"))
                {
                    sound.volume = 0.6f;
                }
                else
                {
                    sound.volume = 1f;
                }
            }
            sound.source.volume = sound.volume; // You can set default volume here
            sound.source.pitch = sound.pitch; // You can set default pitch here
            sound.source.outputAudioMixerGroup = sound.mixerGroup;
            sounds.Add(sound);
        }
    }
    Sound RandomizeSound(Sound sound, float volmin, float volmax, float pitmin, float pitmax)
    {
        sound.source.volume = Random.Range(volmin, volmax);
        sound.source.pitch = Random.Range(pitmin, pitmax);
        return sound;

    }

    // Play a sound by name
    public void PlaySound(string name, bool randomize = false)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        else
        {
            if (randomize)
            {
                sound = RandomizeSound(sound, 0.1f, 0.5f, 0.8f, 1.2f);
            }
            //sound.source.Play();
            sound.source.PlayOneShot(sound.clip);

        }         
    }
    public void StopSound(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        else
        {
            sound.source.Stop();
            
        }
    }
}
