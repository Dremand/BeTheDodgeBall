using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueTeamScore : MonoBehaviour {
    private Text theBlueText;
	// Use this for initialization
	void Start () {
        theBlueText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(BallProjectile.bluePlayerDeath == 1)
        {
            theBlueText.text = "Blue Players Left: 1";  
        }
        if(BallProjectile.bluePlayerDeath == 2)
        {
            theBlueText.text = "Blue Players Left: 2";
        }
	}
}
