using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    float minutes, seconds = 0f;
    float endMinutes, endSeconds;
    float levelTime;
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
        levelTime = (int)Time.timeSinceLevelLoad;
        GameManager.Instance.levelTime += seconds;
        timerText.SetText(minutes.ToString("00") + " : " + seconds.ToString("00"));
        Debug.Log("SECONDS: " + levelTime);
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
        if (levelTime <= 90)
        {
            GameManager.Instance.timeBonus = 300;
        } else if (levelTime > 90 && levelTime <= 150)
        {
            GameManager.Instance.timeBonus = 230;
        }
        else if (levelTime > 150 && levelTime <= 210)
        {
            GameManager.Instance.timeBonus = 170;
        } else if (levelTime > 210)
        {
            GameManager.Instance.timeBonus = 120;
        }

        
    }
}
