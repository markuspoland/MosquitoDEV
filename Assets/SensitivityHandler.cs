using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SensitivityHandler : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonRight()
    {
        if(textMesh.text != "500")
        {
            int sensIncrement = 100;
            int sensValue = Convert.ToInt32(textMesh.text);
            sensValue += sensIncrement;
            textMesh.SetText(sensValue.ToString());
            GameManager.Instance.horizontalSensitivity = sensValue;
            GameManager.Instance.verticalSensitivity = sensValue;
        }
    }

    public void ButtonLeft()
    {
        if (textMesh.text != "-500")
        {
            int sensDecrement = 100;
            int sensValue = Convert.ToInt32(textMesh.text);
            sensValue -= sensDecrement;
            textMesh.SetText(sensValue.ToString());
            GameManager.Instance.horizontalSensitivity = sensValue;
            GameManager.Instance.verticalSensitivity = sensValue;
        }
    }
}
