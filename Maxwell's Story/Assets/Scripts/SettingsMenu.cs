using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private SettingsSaver config;
    [SerializeField] private AudioMixer audioMixer;
    private GameSettings settings => config.GameSettings;

    [Header("Inputs")]
    [SerializeField] private Slider sensitivity;

    [Header("Audio")]
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider SFXVolume;

    private void Start()
    {
        InitializeVariables();
    }

    private void OnDisable()
    {
        config.SaveSettings();
    }

    public void SetSensitivity(float value) => settings.sensitivity = value;

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
        settings.musicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
        settings.SFXVolume = volume;
    }

    private void InitializeVariables()
    {
        sensitivity.value = settings.sensitivity;

        musicVolume.value = settings.musicVolume;

        SFXVolume.value = settings.SFXVolume;
    }
}
