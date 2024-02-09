using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundsSlider;
    [SerializeField] AudioMixer masterMixer;
    private void Start()
    {
        SetMasterVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
        SetSoundsVolume(PlayerPrefs.GetFloat("SavedSoundsVolume", 100));
        SetMusicVolume(PlayerPrefs.GetFloat("SavedMusicVolume", 100));

    }
    //zmienia glosnosc (nastepne 3)
    public void SetMasterVolume(float _value)
    {
        if (_value < 1)
            _value = .001f;

        masterSlider.value = _value;
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
    }
    public void SetMusicVolume(float _value)
    {
        if (_value < 1)
            _value = .001f;

        musicSlider.value = _value;
        PlayerPrefs.SetFloat("SavedMusicVolume", _value);
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(_value / 100) * 20f);
    }
    public void SetSoundsVolume(float _value)
    {
        if (_value < 1)
            _value = .001f;

        soundsSlider.value = _value;
        PlayerPrefs.SetFloat("SavedSoundsVolume", _value);
        masterMixer.SetFloat("SoundsVolume", Mathf.Log10(_value / 100) * 20f);
    }
    public void SetMasterFromSlider()
    {
        SetMasterVolume(masterSlider.value);
    }
    public void SetMusicFromSlider()
    {
        SetMusicVolume(musicSlider.value);
    }
    public void SetSoundsFromSlider()
    {
        SetSoundsVolume(soundsSlider.value);
    }

}
