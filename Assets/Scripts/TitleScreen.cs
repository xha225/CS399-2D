using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public AudioClip buttonClickSFX;
    public AudioClip buttonSelectSFX;

    private AudioSource menuAudioSource;
    private AudioSource cameraAudioSource;
    public Slider musicVol;
    public Toggle sfxToggle;

    public void Start()
    {
        // Reference to the AudioSource component     
        menuAudioSource = GetComponent<AudioSource>();
        // Reference to the AudioSource component of the main camera   
        cameraAudioSource = Camera.main.GetComponent<AudioSource>();
        // Set slider value based on audio source volume
        musicVol.value = cameraAudioSource.volume;
        // Set SFX toggle value based on config value
        sfxToggle.isOn = Configs.SFX;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // Plays the click sound effect
    public void OnButtonClickSFX()
    {
        // Play the button click sound clip
        if (Configs.SFX)
        {
            menuAudioSource.PlayOneShot(buttonClickSFX);
        }
    }

    // Plays the select sound effect
    public void OnButtonSelectSFX()
    {
        // Play the button select sound clip
        if (Configs.SFX)
        {
            menuAudioSource.PlayOneShot(buttonSelectSFX);
        }
            
    }

    public void OnVolChange()
    {
        // Set audio source volume with slider
        cameraAudioSource.volume = musicVol.value;    
    }

    // Enable/Disable SFX
    public void OnSFXToggle()
    {
        if (Configs.SFX)
            Configs.SFX = false;
        else
            Configs.SFX = true;
    }
}
