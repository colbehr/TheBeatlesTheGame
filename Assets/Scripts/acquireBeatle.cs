using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acquireBeatle : MonoBehaviour
{
    GameObject Player;
    bool acquired = false;
    public AudioClip foundLine;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void OnTriggerEnter(Collider c){
        if (c.tag == "Player" && !acquired){
            acquired = true;
            Player.GetComponent<PlayerState>().addBeatle();
            GetComponent<FollowAI>().enabled = true;
            
            GetComponents<AudioSource>()[1].PlayOneShot(foundLine);
            //play voiceline?
            //play animation?
        }
    }

}

