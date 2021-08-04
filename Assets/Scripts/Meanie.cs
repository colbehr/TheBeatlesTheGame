using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Meanie : MonoBehaviour
{
    private Vector3[] points;
    private int destination = 0;
    private NavMeshAgent agent;
    private GameObject player;
    public bool chasing;
    private NavMeshHit hit;
    private bool alerted;

    void Start()
    {
        chasing = false;
        alerted = false;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");

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
        // Alarm sounded, player location is known
        if (alerted)
        {
            agent.destination = player.transform.position;
            chasing = true;
        }

        else
        {

            float angle = Vector3.Angle(player.transform.position - transform.position, transform.forward);
            float dist = Vector3.Distance(gameObject.transform.position, player.transform.position);

            // Catch the player
            if (dist < 1)
            {
                player.gameObject.GetComponent<characterMovement>().Caught();
            }

            // Currently chasing the player
            if (chasing)
            {
                // Lost sight of the player
                if (agent.Raycast(player.transform.position, out hit) || dist > 50 || !player.activeSelf)
                {
                    chasing = false;
                    GotoNextPoint();
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
}
