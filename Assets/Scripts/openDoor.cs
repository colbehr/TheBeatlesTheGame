using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    private GameObject Player;
    Animator anim;
    bool canEnter = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectsWithTag("Player")[0];

    }

    // Update is called once per frame
    void Update()
    {
        if (canEnter)
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
            canEnter = true;
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            Player.transform.parent.GetChild(2).GetComponent<UIController>().playerInteractUIToggle(false);
            canEnter = false;
        }
    }
    void interact(){
        if (Input.GetKeyDown(KeyCode.E))
        {  
            anim.SetBool("Open", true);
        }
    }

}
