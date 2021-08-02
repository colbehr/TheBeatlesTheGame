using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Sprite sprite1_0;
    public Sprite sprite2_0;
    public Sprite sprite3_0;
    public Sprite sprite4_0;
    public Sprite sprite1_1;
    public Sprite sprite2_1;
    public Sprite sprite3_1;
    public Sprite sprite4_1;
    public Sprite eyesOpen;
    public Sprite eyesClosed;
    public Text timeText;
    public Image Eyes;
    private PlayerState playerState;

    void Start()
    {
        playerState = GameObject.FindWithTag("Player").GetComponent<PlayerState>();

    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime(playerState.timeLeft);
        //take this beatles gathered number and display the appropriate number of icons
        if (playerState.beatlesGathered < 1)
        {
            playerState.beatlesGathered = 0;
            image1.sprite = sprite1_1;
            image2.sprite = sprite2_1;
            image3.sprite = sprite3_1;
            image4.sprite = sprite4_1;
        }
        else if (playerState.beatlesGathered == 1)
        {
            image1.sprite = sprite1_0;
            image2.sprite = sprite2_1;
            image3.sprite = sprite3_1;
            image4.sprite = sprite4_1;
        }
        else if (playerState.beatlesGathered == 2)
        {
            image1.sprite = sprite1_0;
            image2.sprite = sprite2_0;
            image3.sprite = sprite3_1;
            image4.sprite = sprite4_1;
        }
        else if (playerState.beatlesGathered == 3)
        {
            image1.sprite = sprite1_0;
            image2.sprite = sprite2_0;
            image3.sprite = sprite3_0;
            image4.sprite = sprite4_1;
        }
        else if (playerState.beatlesGathered >= 4)
        {
            playerState.beatlesGathered = 4;
            image1.sprite = sprite1_0;
            image2.sprite = sprite2_0;
            image3.sprite = sprite3_0;
            image4.sprite = sprite4_0;
        }
        if (playerState.hidden)
        {
            Eyes.sprite = eyesClosed;

        }
        else
        {
            Eyes.sprite = eyesOpen;

        }
    }
    void DisplayTime(float timeDisplay)
    {
        //if time is less than 0, make it zero so we dont show a negatime
        if (timeDisplay < 0)
        {
            timeDisplay = 0;
        }
        float mins = Mathf.FloorToInt(timeDisplay / 60);
        float secs = Mathf.FloorToInt(timeDisplay % 60);
        timeText.text = string.Format("{0}:{1:00}", mins, secs);
    }
}
