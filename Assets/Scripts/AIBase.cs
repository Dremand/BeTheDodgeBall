using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//blue player
public enum EnemyStates
{
    Wandering,
    Chase,
    GoToBall,
    ThrowBall,
    LineUp
}


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
public class AIBase : MonoBehaviour
{
    public bool IamInLineUp = false;
    public bool ballThrownByMe = false;
    public bool iAmHit = false;
    public static AIBase instance;
    public AudioClip ballThrowSound;
    public float wanderRadius = 5.0f;
    public float attackRange = 2.0f;
    public float lookAngle = 10.0f;
    public float EnemyMaxHealth = 100;
    public float EnemyHealth = 100;
    public int moreDamage;
    public Vector3 ranDest2;
    public float acceleration = 2.0f;
    public float maxSpeed = 20.0f;
    public GameObject RedAITrigger;
    public GameObject BlueAITrigger;

    public float curSpeed = 0.0f;
    public static bool blueTeamBallActive = false;
    private NavMeshAgent navAgent;
    private Vector3 ranDest;
    [SerializeField]
    private EnemyStates enemyState = EnemyStates.Wandering;
    private GameObject target;
    [SerializeField]
    private float attackSpeed = 3.0f;
    private float attackTimer;
    private Vector3 pickUpTarget;


    private Transform[] patrolPaths; //array to store where we can walk to for patrolling 
    private Transform[] lineUpPath;
    public int currentNode;
    public int currentLineUpNode;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        ranDest = transform.position;
        ranDest2 = transform.position;
        attackTimer = attackSpeed;
        //pickUpTarget = GetComponentInChildren<Vector3>();
    }

    private void Start()
    {
        EnemyPath ep = FindObjectOfType<EnemyPath>();
        if (ep)
        {
            patrolPaths = ep.GetPaths();
        }
        EnemyPathLineUp eplu = FindObjectOfType<EnemyPathLineUp>();
        if (eplu)
        {
            lineUpPath = eplu.GetPaths();
        }
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        switch (enemyState)
        {
            case EnemyStates.Wandering:
                {
                    //check if we have reached our desitnation
                    if ((ranDest - transform.position).magnitude < 2.0f)
                    {
                        //Debug.Log("At Target Node");
                        //Pick a random path between the next 3 in the array
                        currentNode = (currentNode + Random.Range(1, 3)) % (patrolPaths.Length);
                        //Go to next node in array
                        ranDest = patrolPaths[currentNode].position;
                        navAgent.SetDestination(ranDest);
                        //LineUpNow();
                    }
                    else
                    {
                        navAgent.SetDestination(ranDest);
                    }
                    //otherwise do nothing (let the enemy keep moving)
                }
                break;

            //we have found an enemy!
            case EnemyStates.Chase:
                {
                    //as long as target isn't dead
                    if (target)
                    {
                        //if close enough to target then attack!
                        if ((target.transform.position - transform.position).magnitude < attackRange)
                        {
                            enemyState = EnemyStates.GoToBall;
                        }
                        else
                        {
                            //set destination towards our enemy!
                            SlowlyIncreaseSpeed();
                            navAgent.SetDestination(target.transform.position);
                        }
                    }
                }
                break;

            case EnemyStates.GoToBall:
                {
                    if ((target.transform.position - transform.position).magnitude < attackRange)
                    {
                        //if we are in range
                        if (target && attackTimer <= 0)
                        {
                            //Disable player if there is a player
                            if (target.GetComponent<PlayerMove>())
                            {
                                if (target.GetComponent<PlayerMove>().enabled == true)
                                {
                                    target.GetComponent<PlayerMove>().enabled = false;
                                }
                            }

                            //pick up ball
                            target.transform.position = pickUpTarget;

                            //look at opposite team
                            transform.LookAt(RedAITrigger.transform);

                            //throw the ball
                            enemyState = EnemyStates.ThrowBall;

                            //reset attack timer
                            attackTimer = attackSpeed;
                        }
                    }
                    else
                    {
                        enemyState = EnemyStates.Chase;
                    }
                }
                break;
            case EnemyStates.ThrowBall:
                {
                    ballThrownByMe = true;
                    target.GetComponent<BallProjectile>().ThrowBallBlue();
                    if (target.GetComponent<PlayerMove>())
                    {
                        target.GetComponent<PlayerMove>().enabled = true;
                        MusicPlayer.instance.PlaySoundEffects(ballThrowSound);
                        //Invoke("ActivateBall", 3f);
                    }
                }
                break;
            case EnemyStates.LineUp:
                {
                    navAgent.SetDestination(ranDest2);
                }
                break;
        }

    }
    public void LineUpNow()
    {
            Debug.Log("BlueHitLineUp");
            enemyState = EnemyStates.LineUp;
            IamInLineUp = true;
    }
    public void ActivateBall()
    {
        target.GetComponent<PlayerMove>().enabled = true;
    }

    protected virtual void OnTriggerEnter(Collider c)
    {
        if(c.tag == "BlueAITrigger")
        {
            if (curSpeed < 1.0f)
            {
               enemyState = EnemyStates.Wandering;
            }
        }
        if(c.tag == "RedAITrigger")
        {
            if (curSpeed < 1.5f)
            {
                enemyState = EnemyStates.Wandering;
            }
            else if (curSpeed > 1.5f)
            {
                BallProjectile.bluePlayerDeath++;
                Debug.Log("BlueWalkedOverLineLineUp");
                LineUpNow();
                IamInLineUp = true;
            }
        }
        //if you don't want to use tag you can also use check the object type (i.e. get component<>())
        //checks of the ball is tagged ball
        if (c.tag == "Ball")
        {
            //set our target to the ball
            target = c.gameObject;
            pickUpTarget = target.transform.position;
            pickUpTarget.y += 1.0f;
            //switch the state to chase
            enemyState = EnemyStates.Chase;

        }
    }

    protected virtual void OnTriggerExit(Collider c)
    {
        //check if player is outside our radius (optional) 
        if (c.tag == "Ball")
        {
            //change state back to wandering 
            enemyState = EnemyStates.Wandering;
            curSpeed = 0.0f;
            //clear enemy target
            target = null;
        }
    }
    public void BackToWonderingState()
    {
        enemyState = EnemyStates.Wandering;
    }
    public void SlowlyIncreaseSpeed()
    {
        transform.Translate(Vector3.forward * curSpeed * Time.deltaTime);
        curSpeed += acceleration * Time.deltaTime;
        if (curSpeed > maxSpeed)
            curSpeed = maxSpeed;
    }
}