using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{
    //audio
    public AudioClip openClip;
    public AudioClip closeClip;

    //cameras
    //hidecamera has to be set in inspector
    public Camera HideCamera;
    private Camera MainCamera;
    //speed for turning in the hiding spot
    public float rotSpeed = 100f;
    private GameObject Player;
    bool canEnter = false;
    bool inHiding = false;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //this grabs the first child's animator component, so the animatable component eg. lid/door/etc. should be the first child. 
        anim = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        MainCamera = Camera.main;
        MainCamera.enabled = true;
        HideCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //we have to interact
        if (canEnter)
        { 
            interact();
        }
        //camera rotate according to mouse
        if (inHiding)
        {
            float h = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
            HideCamera.transform.Rotate(0, h, 0);
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            // the third child of the players parent is the canvas;
            Player.transform.parent.GetChild(2).GetComponent<UIController>().playerInteractUIToggle(true);
            canEnter = true;
            //play sound
            GetComponent<AudioSource>().PlayOneShot(openClip);

            anim.SetBool("Open", true);
            //show hide ui
        }
    }
    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            Player.transform.parent.GetChild(2).GetComponent<UIController>().playerInteractUIToggle(false);
            canEnter = false;
            GetComponent<AudioSource>().PlayOneShot(closeClip);
            anim.SetBool("Open", false);
            // hide hide ui
        }
    }

    void interact()
    {
        // change main camera to this camera 
        if (Input.GetKeyDown(KeyCode.E) && !inHiding)
        {
            // hide player model
            Player.SetActive(false);
            // print("Player Gets in");
            MainCamera.enabled = false;
            //we have to swap the audio listener from the player to the new camera because we are hiding the main character
            Player.GetComponent<AudioListener>().enabled = false;
            HideCamera.enabled = true;
            HideCamera.GetComponent<AudioListener>().enabled = true;
            inHiding = true;
        }
        else if (inHiding && Input.GetKeyDown(KeyCode.E))
        {
            Player.SetActive(true);
            // print("Player Leaves");
            MainCamera.enabled = true;
            Player.GetComponent<AudioListener>().enabled = true;
            HideCamera.enabled = false;
            HideCamera.GetComponent<AudioListener>().enabled = false;
            inHiding = false;
        }
    }
}
