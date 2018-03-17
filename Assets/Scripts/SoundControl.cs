using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{

    public Slider volumeControl;
    public Slider SoundFXCOntrol;
    //public LevelManager levelManage;

    private MusicPlayer musicManage;

    // Use this for initialization
    void Start()
    {
        musicManage = GameObject.FindObjectOfType<MusicPlayer>();
        volumeControl.value = PlayerPrefsManage.GetMasterVolume();
        SoundFXCOntrol.value = PlayerPrefsManage.GetSoundFXVolume();
    }

    // Update is called once per frame
    void Update()
    {
        if (musicManage.GetMusicVolume() != volumeControl.value)
        {
            musicManage.SetMusicVolume(volumeControl.value);
            PlayerPrefsManage.SetMasterVolume(volumeControl.value);
        }

        if (musicManage.GetSoundFXVolume() != SoundFXCOntrol.value)
        {
            musicManage.SetSoundFXVolume(SoundFXCOntrol.value);
            PlayerPrefsManage.SetSoundFXVolume(SoundFXCOntrol.value);
        }

    }

    public void SaveExit()
    {
        PlayerPrefsManage.SetMasterVolume(volumeControl.value);
        PlayerPrefsManage.SetSoundFXVolume(SoundFXCOntrol.value);
    }
    public void SetDefaults()
    {
        volumeControl.value = 0.8f;
        SoundFXCOntrol.value = 0.6f;
    }
}

