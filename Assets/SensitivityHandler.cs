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
        if(textMesh.text != "500")
        {
            sensValue = int.Parse(textMesh.text);
            sensValue += sensIncrement;
            GameManager.Instance.horizontalSensitivity = sensValue;
            GameManager.Instance.verticalSensitivity = sensValue;
            textMesh.SetText(sensValue.ToString());
        }
    }

    public void ButtonLeft()
    {
        if (textMesh.text != "-500")
        {
            
            sensValue = int.Parse(textMesh.text);
            sensValue -= sensIncrement;
            GameManager.Instance.horizontalSensitivity = sensValue;
            GameManager.Instance.verticalSensitivity = sensValue;
            textMesh.SetText(sensValue.ToString());
        }
    }
}
