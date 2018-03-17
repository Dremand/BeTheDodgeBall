using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedTeamScore : MonoBehaviour {
    private Text redTeamText;
	// Use this for initialization
	void Start () {
        redTeamText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if(BallProjectile.redPlayerDeath == 1)
        {
            redTeamText.text = "Red Teams Players Left: 1";
        }
        if(BallProjectile.redPlayerDeath == 2)
        {
            redTeamText.text = "Red Teams Players Left: 2";
        }
	}
}
