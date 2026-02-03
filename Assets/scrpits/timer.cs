using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
       public enum TimerFormats
    {
        Whole,
        TenthDecimal,
        HundrethsDecimal
    }

    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countdown;

    [Header("Limit Settings")]
    public bool haslimit;
    public float timerLimit;
    
    [Header("format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();
        // Start is called before the first frame update
    void Start()
    {
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrethsDecimal, "0.00");
    }

    // Update is called once per frame
    void Update()
    {
         currentTime = countdown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

         if (haslimit && ((countdown && currentTime <= timerLimit) || (!countdown && currentTime >= timerLimit)))
         {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;
            enabled = false;
         }

         SetTimerText();
    }

    private void SetTimerText() 
    {
       timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
        

    }

 
}
