using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallProjectile : MonoBehaviour
{
    public AudioClip ballHitSound;
    public bool m_isRunning = false;
    public AIBase blueAIone;
    public AIBase blueAItwo;
    public EnemyBase redAIone;
    public EnemyBase redAItwo;
    public static bool ballIsActive = false;
    public static int bluePlayerDeath = 0;
    public static int redPlayerDeath = 0;

    private Rigidbody m_rb = null;
    private GameObject target;

    
    AIBase[] blues = new AIBase[2];
    EnemyBase[] reds = new EnemyBase[2];
    // Use this for initialization
    void Start()
    {
        blueAIone = FindObjectOfType<AIBase>();
        blueAItwo = FindObjectOfType<AIBase>();
        redAIone = FindObjectOfType<EnemyBase>();
        redAItwo = FindObjectOfType<EnemyBase>();
        m_rb = GetComponent<Rigidbody>();
        bluePlayerDeath = 0;
        redPlayerDeath = 0;
        
        blues[0] = blueAIone;
        blues[1] = blueAItwo;
        
        reds[0] = redAIone;
        reds[1] = redAItwo;
    }

    // Update is called once per frame
    void Update()
    {
        if(bluePlayerDeath == 2)
        {
            SceneManager.LoadScene("Win");
        }
        if(redPlayerDeath == 2)
        {
            SceneManager.LoadScene("Win");
        }
    }
    public void ThrowBallBlue()
    {
        float throwRange;
        throwRange = Random.Range(0.1f, 10.0f);
        float throwRange2;
        throwRange2 = Random.Range(1.0f, 6.0f);
        float throwRange3;
        throwRange3 = Random.Range(0.1f, 14.0f);
        m_rb.velocity = new Vector3(throwRange, throwRange2, throwRange3);
        AIBase.blueTeamBallActive = true;
        ballIsActive = true;
        //Debug.Log("Active Blue Ball");
    }
    public void ThrowBallRed()
    {
        float throwRange;
        throwRange = Random.Range(-0.1f, -10.0f);
        float throwRange2;
        throwRange2 = Random.Range(1.0f, 6.0f);
        float throwRange3;
        throwRange3 = Random.Range(-0.1f, -14.0f);
        m_rb.velocity = new Vector3(throwRange, throwRange2, throwRange3);
        EnemyBase.redTeamBallActive = true;
        ballIsActive = true;
       // Debug.Log("Active Red Ball");
    }
    public void IamNoLongerHit()
    {
        foreach (EnemyBase ab in reds)
        {
            ab.iAmHit = false;
            Debug.Log("redNoLongerHit");
        }
        foreach (AIBase ab in blues)
        {
            ab.iAmHit = false;
            Debug.Log("blueNoLongerHit");
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FloorOrWall"))
        {
            foreach (EnemyBase ab in reds)
            {
                if (ab.iAmHit == true)
                {
                    Debug.Log("RedHitBallHitGround!");
                    //ab.LineUpNow();
                    //redPlayerDeath++;
                    ab.iAmHit = false;
                }
                ab.ballThrownByMe = false;
            }
            foreach (AIBase ab in blues)
            {
                if (ab.iAmHit == true)
                {
                    Debug.Log("BlueHitBallHitGround!");
                    //ab.LineUpNow();
                    //bluePlayerDeath++;
                    ab.iAmHit = false;
                }
                ab.ballThrownByMe = false;
            }
            MusicPlayer.instance.PlaySoundEffects(ballHitSound);
            Debug.Log("Not Active Ball");
            EnemyBase.redTeamBallActive = false;
            AIBase.blueTeamBallActive = false;
            ballIsActive = false;
        }
        if (collision.gameObject.CompareTag("RedPlayers"))
        {
            //make sure blue team threw the ball
            if (AIBase.blueTeamBallActive == true)
            {
                MusicPlayer.instance.PlaySoundEffects(ballHitSound);
                foreach (EnemyBase ab in reds)
                {
                    ab.iAmHit = true;
                    Debug.Log("redHit");
                    Invoke("IamNoLongerHit", 3f);
                }
            }
        }
        if (collision.gameObject.CompareTag("BluePlayers"))
        {
            //make sure red team threw the ball
            if (EnemyBase.redTeamBallActive == true)
            {
                MusicPlayer.instance.PlaySoundEffects(ballHitSound);
                foreach (AIBase ab in blues)
                {
                    ab.iAmHit = true;
                    Debug.Log("blueHit");
                    Invoke("IamNoLongerHit", 3f);
                }
            }
        }
        if (ballIsActive == true)
        {
            //check to see if ball hits the red player and catches the ball
            if (collision.gameObject.CompareTag("RedPlayers"))
            {
                if (AIBase.blueTeamBallActive == true)
                {
                    if(redAIone.iAmHit == true)
                    {
                        redPlayerDeath++;
                        redAIone.LineUpNow();
                    }
                    if(redAItwo.iAmHit == true)
                    {
                        redPlayerDeath++;
                        redAItwo.LineUpNow();
                    }
                    //foreach (EnemyBase obj in reds)
                    //{
                    //    obj.LineUpNow();
                    //    redPlayerDeath++;
                    //}
                }
                //check to see what blue player threw the ball
                //foreach (AIBase obj in blues)
                //{
                //    if (obj.ballThrownByMe == true)
                //    {
                //        //blue player is now out
                //        Debug.Log("RedCaughtBallBlueIsNowOut");
                //        obj.LineUpNow();
                //        bluePlayerDeath++;
                //        //check to see if red player is in line up
                //        foreach (EnemyBase objs in reds)
                //        {
                //            if (objs.IamInLineUp == true)
                //            {
                //                //make red player go back to wander
                //                objs.BackToWonderingState();
                //                redPlayerDeath -= 1;
                //                Debug.Log("RedPlayerBackInPlay");
                //            }
                //        }
                //    }
                //}
            }
            //check to see if ball hits blue player and catches the ball
            if (collision.gameObject.CompareTag("BluePlayers"))
            {
                if (EnemyBase.redTeamBallActive == true)
                {
                    if (blueAIone.iAmHit == true)
                    {
                        bluePlayerDeath++;
                        blueAIone.LineUpNow();
                    }
                    if (blueAItwo.iAmHit == true)
                    {
                        bluePlayerDeath++;
                        blueAItwo.LineUpNow();
                    }
                    //foreach (AIBase objs in blues)
                    //{
                    //    objs.LineUpNow();
                    //    bluePlayerDeath++;
                    //}
                }
                //check to see what red player threw the ball
                //foreach (EnemyBase obj in reds)
                //{
                //    if (obj.ballThrownByMe == true)
                //    {
                //        //red player is now out
                //        Debug.Log("BlueCaughtBallRedNowOut");
                //        obj.LineUpNow();
                //        redPlayerDeath++;
                //        //check to see if a blue player is in line up
                //        foreach (AIBase objs in blues)
                //        {
                //            if (objs.IamInLineUp == true)
                //            {
                //                //make blue player if one is out go back to wander
                //                objs.BackToWonderingState();
                //                bluePlayerDeath -= 1;
                //                Debug.Log("BluePlayerBackInPlay");
                //            }
                //        }
                //    }
                //}
            }
        }
    }
}
//3 Different ways to write to make player catches make the player that throws go out.
//1:
//if(blueAIone.ballThrownByMe == true)
//{
//    Destroy(blueAIone);
//}
//if(blueAItwo.ballThrownByMe == true)
//{
//    Destroy(blueAItwo);
//}
//2 + 3 used this:
//AIBase[] blues = new AIBase[2];
//EnemyBase[] reds = new EnemyBase[2];
//2:
//for (int i = 0; i < blues.Length; i++) {
//    if(blues[i].ballThrownByMe == true) {
//        Destroy(blues[i]);
//    }
//}
//3:
