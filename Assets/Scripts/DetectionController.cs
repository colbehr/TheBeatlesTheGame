using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
    private GameObject[] meanies;
    private AudioSource music;
    private GameObject eye;
    private PlayerState playerState;
    public AudioClip suspense;
    public AudioClip safe;

    // Start is called before the first frame update
    void Start()
    {
        meanies = GameObject.FindGameObjectsWithTag("Meanie");
        music = GetComponent<AudioSource>();
        playerState = GameObject.FindWithTag("Player").gameObject.GetComponent<PlayerState>();
        // eye = GameObject.Find("Canvas").transform.Find("Image").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        bool chase = false;
        foreach (GameObject meanie in meanies)
        {
            if(meanie.GetComponent<Meanie>().chasing == true)
            {
                chase = true;
                
                break;
            }
        }

        if (chase)
        {
            // eye.SetActive(true);
            playerState.hidden = false;
            if (music.clip.name == "Pepperland" || music.clip.name == "Sea Of Time")
            {
                music.clip = suspense;
                music.Play();
                return;
            }
        }
        else
        {
            // eye.SetActive(false);
            playerState.hidden = true;
            if (music.clip.name == "March of the Meanies")
            {
                music.clip = safe;
                music.Play();
            }
        }
    }
}
