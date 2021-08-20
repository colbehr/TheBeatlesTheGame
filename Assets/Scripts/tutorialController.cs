using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialController : MonoBehaviour
{
    bool active = false;
    bool canTalk = false;
    private GameObject Player;
    public AudioClip talkClip;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (canTalk)
        { 
            interact();
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            // the third child of the players parent is the canvas;
            Player.transform.parent.GetChild(2).GetComponent<UIController>().playerInteractUIToggle(true);
            canTalk = true;
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            Player.transform.parent.GetChild(2).GetComponent<UIController>().playerInteractUIToggle(false);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            canTalk = false;
        }
    }
    void interact()
    {
        // change main camera to this camera 
        if (Input.GetKeyDown(KeyCode.E))
        {
            //play oneshot
            GetComponent<AudioSource>().PlayOneShot(talkClip);
            
            //show text on screen
            if (active)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                active = false;
            } else{
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                active = true;

            }
        }
    }

}
