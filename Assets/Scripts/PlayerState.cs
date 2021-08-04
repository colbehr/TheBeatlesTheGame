using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hidden = true;
    public float timeLeft = 90;
    public int beatlesGathered = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if time is greater than 0, decrement timer
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            timeLeft = 0;
            //reset scene at end of timer
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
}
