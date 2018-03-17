using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BrodcastSong : MonoBehaviour
{

    public UnityAction<string> SongName;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(BrodcastSongName);
    }

    private void BrodcastSongName()
    {
        SongName(GetComponentInChildren<Text>().text);
    }
}