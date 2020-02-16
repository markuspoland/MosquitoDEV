using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTime : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    void Start()
    {
        
    }

    void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.SetText(GameManager.Instance.levelTimerText);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
