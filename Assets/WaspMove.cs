using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspMove : MonoBehaviour
{

    public Transform[] waypoints;
    public BoxCollider sting;
    int currentWaypoint = 0;
    Transform targetWaypoint;
    Transform player;
    Animator anim;
    float speed = 65f;
    float pursueSpeed = 48f;
    float rotSpeed = 10f;
    float distanceFromPlayer;
    float pursueDistance = 100f;
    float pursueCounter;
    bool isPursuing;
    Vector3 playerBarrier;

    WaspState currentState;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentState = WaspState.Patrol;
        pursueCounter = 7f;
        isPursuing = false;
        InvokeRepeating("CheckForPlayer", 0, 2);
        playerBarrier = new Vector3(2, 2, 2);
        sting.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case WaspState.Patrol:
                {
                    if (targetWaypoint == waypoints[waypoints.Length - 1])
                    {
                        currentWaypoint = 0;
                    }

                    if (currentWaypoint < waypoints.Length)
                    {
                        targetWaypoint = waypoints[currentWaypoint];

                        MoveWasp();

                    }


                    pursueCounter += 1f * Time.deltaTime;

                    if (pursueCounter > 10f)
                    {
                        pursueCounter = 10f;
                        CheckDistanceFromPlayer();
                    }
                    
                                   
                   

                    if (distanceFromPlayer < 100f && distanceFromPlayer > 10f)
                    {
                        currentState = WaspState.Pursue;
                    }
                    break;
                }
            case WaspState.Pursue:
                {
                    PursuePlayer();
                    pursueCounter -= 1f * Time.deltaTime;
                    if (pursueCounter <= 0)
                    {
                        distanceFromPlayer = 200f;
                        currentState = WaspState.Patrol;

                    }

                    CheckDistanceFromPlayer();
                    //if (distanceFromPlayer < 10f)
                    //{
                    //    pursueSpeed += 2;
                        
                    //}

                    if (distanceFromPlayer < 7f)
                    {
                        currentState = WaspState.Attack;
                    }
                    
                    
                    break;
                }

            case WaspState.Attack:
                {

                    PursuePlayer();
                    
                    
                    CheckDistanceFromPlayer();

                    if (distanceFromPlayer < 6f)
                    {
                        StartCoroutine(Attack());
                    }

                    
                    break;
                }
        }

                
    }

    void MoveWasp()
    {
        transform.forward = Vector3.RotateTowards(transform.forward, targetWaypoint.position - transform.position, rotSpeed * Time.deltaTime, 0.0f);
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (transform.position == targetWaypoint.position)
        {
            currentWaypoint++;
            targetWaypoint = waypoints[currentWaypoint];
        }
    }

    void CheckForPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void CheckDistanceFromPlayer()
    {
        if (player != null)
        distanceFromPlayer = Vector3.Distance(transform.position, player.position);
    }

    void PursuePlayer()
    {
        transform.forward = Vector3.RotateTowards(transform.forward, player.position - transform.position, rotSpeed * Time.deltaTime, 0.0f);
        transform.position = Vector3.MoveTowards(transform.position, player.position, pursueSpeed * Time.deltaTime);
    }

    public enum WaspState
    {
        Patrol,
        Pursue,
        Attack
    }

    IEnumerator Attack ()
    {
        transform.forward = Vector3.RotateTowards(transform.forward, targetWaypoint.position - transform.position, rotSpeed * Time.deltaTime, 0.0f);
        transform.position = Vector3.MoveTowards(transform.position, player.position + playerBarrier, speed * Time.deltaTime);
        sting.enabled = true;
        anim.SetTrigger("WaspHit");
        yield return new WaitForSeconds(2f);
        sting.enabled = false;
        pursueCounter = 7;
        distanceFromPlayer = 200f;
        currentState = WaspState.Patrol;
    }
}
