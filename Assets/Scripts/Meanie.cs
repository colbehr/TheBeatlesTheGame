using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Meanie : MonoBehaviour
{
    private Vector3[] points;
    private int destination = 0;
    private NavMeshAgent agent;
    private Animator animator;
    private GameObject player;
    public bool chasing;
    private NavMeshHit hit;
    private PlayerState playerState;
    public AudioClip[] steps;
    private bool alerted;
    public AudioClip[] lines;
    bool playing = false;

    void Start()
    {
        chasing = false;
        alerted = false;
        playerState = GameObject.FindWithTag("Player").gameObject.GetComponent<PlayerState>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        animator.Play("RunForwardsJump_Frame01", 0);
        // Set up patrol points based of child points
        points = new Vector3[transform.Find("Path").childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.Find("Path").GetChild(i).position;
        }
        GotoNextPoint();
    }

    void Update()
    {
        animator.SetFloat("MoveX", agent.velocity.x/3);
        animator.SetFloat("MoveZ", agent.velocity.z/3);
        // Alarm sounded, player location is known
        if (alerted)
        {
            agent.destination = player.transform.position;
            chasing = true;
            playerState.hidden = false;
            
        }
        else
        {

            float angle = Vector3.Angle(player.transform.position - transform.position, transform.forward);
            float dist = Vector3.Distance(gameObject.transform.position, player.transform.position);

            // Catch the player
            if (dist < 1 && chasing)
            {
                player.gameObject.GetComponent<characterMovement>().Caught();
            }

            // Currently chasing the player
            if (chasing)
            {
                if (!playing)
                {
                    GetComponents<AudioSource>()[1].PlayOneShot(lines[Random.Range(0,steps.Length-1)]);
                    playing = true;
                } 
                // Lost sight of the player
                if (agent.Raycast(player.transform.position, out hit) || dist > 50 || !player.activeSelf)
                {
                    playerState.hidden = true;

                    chasing = false;
                    GotoNextPoint();
                    playing = false;

                }

                // Keep chasing
                else
                {
                    agent.destination = player.transform.position;
                }
            }

            // Player spotted
            else if (!agent.Raycast(player.transform.position, out hit) && player.activeSelf &&
                dist < 30 && angle > -70 && angle < 70)
            {
                playerState.hidden = false;
                chasing = true;
                agent.destination = player.transform.position;
            }

            else if (dist > 1 || !player.activeSelf)
            {
                if (!chasing)
                {
                    if (!agent.pathPending && agent.remainingDistance < 0.5f)
                    {
                        GotoNextPoint();
                    }
                }
                else
                {
                    playerState.hidden = true;
                    chasing = false;
                    GotoNextPoint();
                }
            }
        }
    }


    private void GotoNextPoint()
    {
        // Makes sure there's actually a point to go to
        if (points.Length != 0)
        {
            // Move to the next point
            agent.destination = points[destination];

            // Go to the next point, restart when path is completed
            destination = (destination + 1) % points.Length;
        }
    }

    public void AlarmSounded()
    {
        alerted = true;
    }

    public void AlarmCancelled()
    {
        alerted = false;
        agent.destination = points[destination];
    }
    public void playStepSound(){
        GetComponent<AudioSource>().PlayOneShot(steps[Random.Range(0,steps.Length)]);
    }

}
