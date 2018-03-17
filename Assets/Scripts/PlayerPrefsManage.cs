using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManage : MonoBehaviour
{

    const string Master_Volume_Key = "master_volume";
    const string SoundFX_Volume_Key = "SoundFX_volume";
    const string Level_Key = "level_unlocked_";

    public static void SetMasterVolume(float volume)
    {
        if (volume > 0f && volume < 1f)
        {
            PlayerPrefs.SetFloat(Master_Volume_Key, volume);
        }
        else
        {
            Debug.Log("Master volume out of Range.");
        }
    }
    public static void SetSoundFXVolume(float volume)
    {
        if (volume > 0f && volume < 1f)
        {
            PlayerPrefs.SetFloat(SoundFX_Volume_Key, volume);
        }
        else
        {
            Debug.Log("Master volume out of Range.");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(Master_Volume_Key);
    }
    public static float GetSoundFXVolume()
    {
        return PlayerPrefs.GetFloat(SoundFX_Volume_Key);
    }
}
