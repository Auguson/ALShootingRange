using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private int time = 0;

    public TMP_Text timer;
    public TMP_Text highscore;

    void Start()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highscore.text = PlayerPrefs.GetInt("Highscore").ToString();
        }
        else
        {
            highscore.text = "0";
        }
    }

    public void StartTimer()
    {
        time = 0;
        InvokeRepeating("IncrementTime", 1f, 1f);
    }

    public void StopTimer()
    {
        CancelInvoke();

        if (!PlayerPrefs.HasKey("Highscore") || PlayerPrefs.GetInt("Highscore") < time)
        {
            SetHighscore();
        }
    }

    public void SetHighscore()
    {
        PlayerPrefs.SetInt("Highscore", time);
        highscore.text = time.ToString();
    }

    public void ClearHighscores()
    {
        PlayerPrefs.DeleteKey("Highscore");
        highscore.text = "0";
    }

    void IncrementTime()
    {
        time += 1;
        timer.text = "Time: " + time;
    }

    void Update()
    {

    }
}