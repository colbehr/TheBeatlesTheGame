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
    private bool chasing;

    void Start()
    {
        chasing = false;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");

        // Set up patrol points based of child points
        points = new Vector3[gameObject.transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = gameObject.transform.GetChild(i).position;
        }
        GotoNextPoint();
    }

    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > 10 || !player.active)
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
                GotoNextPoint();
            }
        }
        else
        {
            chasing = true;
            agent.destination = player.transform.position;
        }
    }

    void GotoNextPoint()
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
}
