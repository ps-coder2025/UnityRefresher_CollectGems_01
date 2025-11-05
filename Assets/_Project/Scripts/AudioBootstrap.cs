using UnityEngine;
using UnityEngine.Audio;

public class AudioBootstrap : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource musicSource;

    void Start()
    {
        // Load saved volumes (defaults)
        float music = PlayerPrefs.GetFloat("MusicVolDB", -10f);
        float sfx = PlayerPrefs.GetFloat("SFXVolDB", -10f);
        mixer.SetFloat("MusicVol", music);
        mixer.SetFloat("SFXVol", sfx);
        

        // Autoplay if clip assigned
        if (musicSource && musicSource.clip) musicSource.Play();
    }
}
