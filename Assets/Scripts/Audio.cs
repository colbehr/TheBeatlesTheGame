using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private GameObject[] meanies;
    private AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        meanies = GameObject.FindGameObjectsWithTag("Meanie");
        music = GetComponent<AudioSource>();
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

        if (chase && music.clip.name == "Pepperland")
        {
            music.clip = Resources.Load<AudioClip>("March of the Meanies");
            music.Play();
            return;
        }

        else if (!chase && music.clip.name == "March of the Meanies")
        {
            music.clip = Resources.Load<AudioClip>("Pepperland");
            music.Play();
        }
    }
}
