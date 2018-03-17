using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManage : MonoBehaviour {
    public GameObject blueTeamWin;
    public GameObject redTeamWin;
	// Use this for initialization
	void Start () {
		if(BallProjectile.redPlayerDeath == 2)
        {
            blueTeamWin.SetActive(true);
        }
        if(BallProjectile.bluePlayerDeath == 2)
        {
            redTeamWin.SetActive(true);
        }
	}
}
