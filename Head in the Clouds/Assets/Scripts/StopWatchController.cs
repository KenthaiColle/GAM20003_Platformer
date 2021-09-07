using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StopWatchController : MonoBehaviour
{
    //code from https://answers.unity.com/questions/1514650/how-to-make-a-stopwatch-that-restarts-every-time-t.html
    float initialTime;

    public GameObject player;
    public bool gameFinished = false;


    bool stopwatchActive = false;
    float currentTime;
    public Text currentTimeText;
    public Text highScoreTimeText;

    public TimeSpan time;

    //Need a link to player's script here e.g. Game finish bool

    void Start()
    {
        currentTime = 0;
        
        Debug.Log(gameFinished);
        stopwatchActive = true;
        PlayerPrefs.SetFloat("HighscoreTime", 0);
        initialTime = Time.time;
        if (PlayerPrefs.HasKey("HighscoreTime") == false)
        {
            PlayerPrefs.SetFloat("HighscoreTime", 0);
        }
        else
        {

        }
        
    }

    private void Update()
    {
        gameFinished = player.GetComponent<PlayerController>().gameFinished;
        if (stopwatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\.fff"); //shortcut for minute, second, milliseconds.
        

        if (gameFinished == true)//Change this to game finish bool to record time.
        {
            SaveHighScoreTime();
        }

    }


    public void SaveHighScoreTime()
    {
        
        float TimeTaken = Time.time - initialTime;
        if ((TimeTaken) > PlayerPrefs.GetFloat("HighscoreTime"))//">" or "<" I'm not sure.. If the highscore is the highest time taken then ">" is right , otherwise change it to "<"
        {
            print("Highscore time is " + TimeTaken);
            PlayerPrefs.SetFloat("HighscoreTime", TimeTaken);
            highScoreTimeText.text = time.ToString(@"mm\:ss\.fff");
        }
        transform.position = new Vector3(0, 0, 0);
        initialTime = Time.time;//Fixed it
    }



    public void StartStopWatch()
    {
        stopwatchActive = true;
    }

    public void StopStopwatch()
    {
        stopwatchActive = false;
    }


}
