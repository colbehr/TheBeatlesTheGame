using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FollowAI : MonoBehaviour
{
    public GameObject followObject;
    NavMeshAgent agent;
    Animator animator;
    public AudioClip[] steps;

    public float maxTime = 1;
    float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            agent.destination = followObject.transform.position;    
            timer = maxTime;        
        }
        animator.SetFloat("MoveX", agent.velocity.x);
        animator.SetFloat("MoveZ", agent.velocity.z);
        
    }

     public void playStepSound(){
        GetComponent<AudioSource>().PlayOneShot(steps[Random.Range(0,steps.Length)]);
    }

}
