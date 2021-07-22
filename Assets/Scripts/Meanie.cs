using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Meanie : MonoBehaviour
{
    private Transform[] points;
    private int destination = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.autoBraking = false;

        // Set up patrol points based of child points
        points = new Transform[gameObject.transform.GetChild(0).transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = gameObject.transform.GetChild(0).transform.GetChild(i).transform;
        }
        GotoNextPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {
        // Makes sure there's actually a point to go to
        if (points.Length != 0)
        {
            // Move to the next point
            agent.destination = points[destination].position;

            // Go to the next point, restart when path is completed
            destination = (destination + 1) % points.Length;
        }
    }
}
