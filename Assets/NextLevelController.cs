using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour
{

    public string scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        print("Next level UI");
        print("Press 'E' to go to the next level.");
    }

    void OnTriggerStay(Collider c)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }

}
