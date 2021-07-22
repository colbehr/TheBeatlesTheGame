using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        GetComponent<AudioSource>().Play();
        Invoke("PlayPress", 0.5f);
    }

    public void Quit()
    {
        GetComponent<AudioSource>().Play();
        Invoke("QuitPress", 0.5f);
    }

    private void PlayPress()
    {
        SceneManager.LoadScene("tutorial");
    }

    private void QuitPress()
    {
        Application.Quit();
    }
}
