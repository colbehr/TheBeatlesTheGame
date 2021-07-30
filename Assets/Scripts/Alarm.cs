using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alarm : MonoBehaviour
{

    private GameObject player;
    private GameObject[] meanies;
    private NavMeshHit hit;
    private NavMeshAgent agent;
    private AudioSource alertNoise;
    private bool alerted;

    void Start()
    {
        meanies = GameObject.FindGameObjectsWithTag("Meanie");
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        alertNoise = GetComponent<AudioSource>();
        alerted = false;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Vector3.Angle(player.transform.position - transform.position, transform.forward);
        float dist = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (!agent.Raycast(player.transform.position, out hit) && player.activeSelf &&
                dist < 30 && angle > -70 && angle < 70)
        {
            if (!alerted)
            {
                alertNoise.Play();
                alerted = true;
            }
            AlertOthers();
        }

        else
        {
            if (alerted)
            {
                alerted = false;
                CancelAlert();
            }
        }
    }

    private void AlertOthers()
    {
        foreach (GameObject meanie in meanies)
        {
            meanie.GetComponent<Meanie>().AlarmSounded();
        }
    }

    private void CancelAlert()
    {
        foreach (GameObject meanie in meanies)
        {
            meanie.GetComponent<Meanie>().AlarmCancelled();
        }
    }
}
