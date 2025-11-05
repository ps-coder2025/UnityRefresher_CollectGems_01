using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettingsUI : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    void OnEnable()
    {
        // Initialize sliders from saved dB -> convert to 0..1
        float musicDB = PlayerPrefs.GetFloat("MusicVolDB", -10f);
        float sfxDB = PlayerPrefs.GetFloat("SFXVolDB", -10f);
        musicSlider.SetValueWithoutNotify(DbTo01(musicDB));
        sfxSlider.SetValueWithoutNotify(DbTo01(sfxDB));
    }

    public void OnMusicChanged(float v01)
    {
        float db = ToDb(v01);
        mixer.SetFloat("MusicVol", db);
        PlayerPrefs.SetFloat("MusicVolDB", db);
    }

    public void OnSfxChanged(float v01)
    {
        float db = ToDb(v01);
        mixer.SetFloat("SFXVol", db);
        PlayerPrefs.SetFloat("SFXVolDB", db);
    }

    // Helpers: 0..1 -> dB (approx, with floor mute)
    float ToDb(float v01)
    {
        if (v01 <= 0.0001f) return -80f; // effectively mute
        return Mathf.Lerp(-30f, 0f, v01); // linear-ish mapping
    }
    float DbTo01(float db) => Mathf.InverseLerp(-30f, 0f, Mathf.Clamp(db, -30f, 0f));
}
