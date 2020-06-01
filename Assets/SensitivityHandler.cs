using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SensitivityHandler : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    int sensIncrement;
    int sensValue;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        sensIncrement = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonRight()
    {
        if(GameManager.Instance.horizontalSensitivity <= 400)
        {
            sensValue = (int)GameManager.Instance.horizontalSensitivity;
            sensValue += sensIncrement;
            GameManager.Instance.horizontalSensitivity = sensValue;
            GameManager.Instance.verticalSensitivity = sensValue;
            textMesh.SetText(sensValue.ToString());
        }
    }

    public void ButtonLeft()
    {
        if (GameManager.Instance.horizontalSensitivity >= -200)
        {
            
            sensValue = (int)GameManager.Instance.horizontalSensitivity;
            sensValue -= sensIncrement;
            GameManager.Instance.horizontalSensitivity = sensValue;
            GameManager.Instance.verticalSensitivity = sensValue;
            textMesh.SetText(sensValue.ToString());
        }
    }
}
