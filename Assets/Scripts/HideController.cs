using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideController : MonoBehaviour
{
    public Camera MainCamera;
    public Camera HideCamera;
    public float rotSpeed = 100f;
    public GameObject Player;

    bool canEnter = false;
    bool inHiding = false;
    GameObject HideAnimate;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        HideAnimate = this.gameObject.transform.GetChild(0).gameObject;
        anim = HideAnimate.GetComponent<Animator>();
        MainCamera = Camera.main;
        MainCamera.enabled = true;
        HideCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //camera rotate according to mouse
        float h = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        HideCamera.transform.Rotate(0, h, 0);
        interact();

    }

    void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Player")
        {
            canEnter = true;
            anim.SetBool("Open", true);
            //show hide ui
        }
    }
    void OnTriggerExit(Collider player)
    {
        if (player.tag == "Player")
        {
            canEnter = false;
            anim.SetBool("Open", false);
            // hide hide ui
        }
    }


    void interact()
    {
        // change main camera to this camera 
        if (Input.GetKeyDown(KeyCode.E) && !inHiding)
        {
            // hide player 
            Player.SetActive(false);
            print("Player Gets in");
            MainCamera.enabled = false;
            HideCamera.enabled = true;
            inHiding = true;
        }
        else if (inHiding && Input.GetKeyDown(KeyCode.E))
        {
            Player.SetActive(true);
            print("Player Leaves");
            MainCamera.enabled = true;
            HideCamera.enabled = false;
            inHiding = false;
        }
    }
}
