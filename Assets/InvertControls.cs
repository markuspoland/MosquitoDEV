using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvertControls : MonoBehaviour
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
        textMesh.SetText("ON");
        GameManager.Instance.invertControls = true;
    }

    public void ButtonLeft()
    {
        textMesh.SetText("OFF");
        GameManager.Instance.invertControls = false;
    }
}
