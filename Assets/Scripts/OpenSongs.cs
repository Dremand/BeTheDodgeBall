using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSongs : MonoBehaviour
{

    public BrodcastSong buttonsPrefab;

    public Transform SongsLocation;

    // Use this for initialization
    void Start()
    {
        List<string> SongNames = MusicPlayer.instance.GetSongNames();
        for (int i = 0; i < SongNames.Count; i++)
        {
            BrodcastSong theButton = Instantiate<BrodcastSong>(buttonsPrefab, SongsLocation);
            theButton.GetComponentInChildren<Text>().text = SongNames[i];
            theButton.SongName += PlaySong;
        }
    }

    public void PlaySong(string songName)
    {
        MusicPlayer.instance.PlaySongs(songName);
    }
}
