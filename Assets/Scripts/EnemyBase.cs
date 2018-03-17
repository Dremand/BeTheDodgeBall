using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//red player
public enum EnemyState
{
    Wandering,
    Chase,
    Attack,
    ThrowBall,
    LineUp
}


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
public class EnemyBase : MonoBehaviour
{
    public bool IamInLineUp = false;
    public bool ballThrownByMe = false;
    public bool iAmHit;
    public static EnemyBase instance;
    public AudioClip ballThrowSound;
    public float wanderRadius = 5.0f;
    public float attackRange = 2.0f;
    public float lookAngle = 10.0f;
    public int moreDamage;
    public static bool redTeamBallActive = false;
    public Vector3 ranDest2;
    public float acceleration = 2.0f;
    public float maxSpeed = 20.0f;
    public GameObject RedAITrigger;
    public GameObject BlueAITrigger;

    public float curSpeed = 0.0f;
    private NavMeshAgent navAgent;
    private Vector3 ranDest;
    
    [SerializeField]
    private EnemyState enemyState = EnemyState.Wandering;
    private GameObject target;
    [SerializeField]
    private float attackSpeed = 3.0f;
    private float attackTimer;
    private Vector3 pickUpTarget;

    private Transform[] patrolPaths; //array to store where we can walk to for patrolling 
    public int currentNode;

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        ranDest = transform.position;
        attackTimer = attackSpeed;
    }

    private void Start()
    {
        EnemyPaths ep = FindObjectOfType<EnemyPaths>();
        if (ep)
        {
            patrolPaths = ep.GetPaths();
        }
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        switch (enemyState)
        {
            case EnemyState.Wandering:
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
                    }
                    else
                    {
                        navAgent.SetDestination(ranDest);
                    }
                    //otherwise do nothing (let the enemy keep moving)
                }
                break;

            //we have found an enemy!
            case EnemyState.Chase:
                {
                    //as long as target isn't dead
                    if (target)
                    {
                        //if close enough to target then attack!
                        if ((target.transform.position - transform.position).magnitude < attackRange)
                        {
                            enemyState = EnemyState.Attack;
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

            case EnemyState.Attack:
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
                            transform.LookAt(BlueAITrigger.transform);
                            //throw the ball
                            enemyState = EnemyState.ThrowBall;
                            //reset attack timer
                            attackTimer = attackSpeed;
                        }
                    }
                    else
                    {
                        enemyState = EnemyState.Chase;
                    }
                }
                break;
            case EnemyState.ThrowBall:
                {
                    ballThrownByMe = true;
                    target.GetComponent<BallProjectile>().ThrowBallRed();
                    if (target.GetComponent<PlayerMove>())
                    {
                        target.GetComponent<PlayerMove>().enabled = true;
                        MusicPlayer.instance.PlaySoundEffects(ballThrowSound);
                        //Invoke("ActivateBall", 3f);
                        
                    }
                }
                break;
            case EnemyState.LineUp:
                {
                    navAgent.SetDestination(ranDest2);
                }
                break;
        }

    }

    protected virtual void OnTriggerEnter(Collider c)
    {
        if(c.tag == "BlueAITrigger")
        {
            if (curSpeed < 1.0f)
            {
                    enemyState = EnemyState.Wandering;
            }
        }
        if(c.tag == "RedAITrigger")
        {
            if (curSpeed < 1.5f)
            {
                enemyState = EnemyState.Wandering;
            }
            else if (curSpeed > 1.5f)
            {
                BallProjectile.redPlayerDeath++;
                Debug.Log("BlueWalkedOverLineLineUp");
                LineUpNow();
                IamInLineUp = true;
            }
        }
        
        //can also check the object type (i.e. get component<>())
        if (c.tag == "Ball")
        {
            //set our target to the player
            target = c.gameObject;
            pickUpTarget = target.transform.position;
            pickUpTarget.y += 1.0f;
            //switch the state to chase 
            enemyState = EnemyState.Chase;
        }
    }

    protected virtual void OnTriggerExit(Collider c)
    {
        //check if player is outside our radius (optional) 
        if (c.tag == "Ball")
        {
            //change state back to wandering 
            enemyState = EnemyState.Wandering;
            curSpeed = 0.0f;
            //clear enemy target
            target = null;
        }
    }

    public void LineUpNow()
    {
            Debug.Log("RedHitLineUp");
            enemyState = EnemyState.LineUp;
            IamInLineUp = true;
    }
    public void BackToWonderingState()
    {
        enemyState = EnemyState.Wandering;
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
    public void SlowlyIncreaseSpeed()
    {
        transform.Translate(Vector3.forward * curSpeed * Time.deltaTime);
        curSpeed += acceleration * Time.deltaTime;
        if (curSpeed > maxSpeed)
            curSpeed = maxSpeed;
    }
}