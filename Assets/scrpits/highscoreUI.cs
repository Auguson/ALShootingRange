using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreUIBonus : MonoBehaviour
{
    public TextMeshProUGUI wave1Text;
    public TextMeshProUGUI wave2Text;
    public TextMeshProUGUI wave3Text;

    void Start()
    {
        // Load best times from PlayerPrefs
        float wave1Best = PlayerPrefs.GetFloat("WaveBest_1", float.MaxValue);
        float wave2Best = PlayerPrefs.GetFloat("WaveBest_2", float.MaxValue);
        float wave3Best = PlayerPrefs.GetFloat("WaveBest_3", float.MaxValue);

        // Display each wave's best time
        wave1Text.text = "Wave 1 Best: " + FormatTime(wave1Best); // + CheckNewBest(1, wave1Best);
        wave2Text.text = "Wave 2 Best: " + FormatTime(wave2Best); // + CheckNewBest(2, wave2Best);
        wave3Text.text = "Wave 3 Best: " + FormatTime(wave3Best); // + CheckNewBest(3, wave3Best);
    }
    
    string FormatTime(float time)
    {
        if (time == float.MaxValue) return "-- : --";
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}