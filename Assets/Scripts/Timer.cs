using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    float minutes, seconds = 0f;
    float endMinutes, endSeconds;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timerText.SetText("00 : 00");
    }

    // Update is called once per frame
    void Update()
    {
        seconds = (int)(Time.timeSinceLevelLoad % 60f);
        minutes = (int)(Time.timeSinceLevelLoad / 60f);
        GameManager.Instance.levelTime += seconds;
        timerText.SetText(minutes.ToString("00") + " : " + seconds.ToString("00"));  
    }

    public void SetLevelTime()
    {
        endSeconds = seconds;
        endMinutes = minutes;
        timerText.SetText(endMinutes.ToString("00") + " : " + endSeconds.ToString("00"));
        GameManager.Instance.levelTimerText = endMinutes.ToString("00") + " : " + endSeconds.ToString("00");
    }

    public void CalculateLevelBonus()
    {
        if (seconds <= 90)
        {
            GameManager.Instance.timeBonus = 300;
        } else if (seconds > 90 && seconds <= 150)
        {
            GameManager.Instance.timeBonus = 230;
        }
        else if (seconds > 150 && seconds <= 210)
        {
            GameManager.Instance.timeBonus = 170;
        } else if (seconds > 210)
        {
            GameManager.Instance.timeBonus = 120;
        }

        
    }
}
