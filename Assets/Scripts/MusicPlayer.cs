using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance = null;

    public AudioClip startSong;
    public AudioClip gameSong;
    public AudioClip endSong;
    public List<AudioClip> _Music;
    public List<AudioClip> PlayList;
    private int MyPlayListIterator;
    public AudioSource gameMusic;
    public AudioSource soundFX;

    void Start()
    {

        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            //add delegate to sceneLoaded for switching scenes
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        MyPlayListIterator = 0;
    }
    private void Update()
    {
        if (!gameMusic.isPlaying)
        {
            gameMusic.PlayOneShot(PlayList[MyPlayListIterator]);
            MyPlayListIterator++;
            if (MyPlayListIterator >= PlayList.Count)
            {
                MyPlayListIterator = 0;
            }
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Start Menu")
        {
            //gameMusic.Stop();
            // gameMusic.PlayOneShot(startSong);

        }
        else if (scene.name == "Game")
        {
            // gameMusic.Stop();
            // gameMusic.PlayOneShot(gameSong);
        }
    }
    public void PlaySoundEffects(AudioClip SoundFeX)
    {
        if (!soundFX.isPlaying)
        {
        soundFX.PlayOneShot(SoundFeX);
        }
    }
    public void SetMusicVolume(float volume)
    {
        if (gameMusic != null)
        {
            gameMusic.volume = volume;
        }
    }
    public void SetSoundFXVolume(float volume)
    {
        if (soundFX != null)
        {
            soundFX.volume = volume;
        }
    }
    public float GetMusicVolume()
    {
        return gameMusic.volume;

    }
    public float GetSoundFXVolume()
    {
        return soundFX.volume;
    }
    public List<string> GetSongNames()
    {
        List<string> MyList = new List<string>();
        for (int i = 0; i < _Music.Count; i++)
        {
            MyList.Add(_Music[i].name);
        }
        return MyList;
    }

    public void PlaySongs(string songName)
    {
        //search through the song array
        for (int i = 0; i < _Music.Count; i++)
        {
            //if the song is found play the song
            if (_Music[i].name == songName)
            {
                gameMusic.Stop();
                gameMusic.PlayOneShot(_Music[i]);
            }
        }
    }
}
