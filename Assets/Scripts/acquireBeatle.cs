using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acquireBeatle : MonoBehaviour
{
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void OnTriggerEnter(Collider c){
        if (c.tag == "Player"){
            Player.GetComponent<PlayerState>().addBeatle();
            GetComponent<FollowAI>().enabled = true;
            //play sound
            //play voiceline?
            //play animation?
        }
    }

}

