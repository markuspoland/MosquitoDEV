using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timerText;
    float minutes, seconds;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        timerText.SetText("00 : 00");
    }

    // Update is called once per frame
    void Update()
    {
        seconds = (int)(Time.time % 60f);
        minutes = (int)(Time.time / 60f);
        timerText.SetText(minutes.ToString("00") + " : " + seconds.ToString("00"));  
    }
}
