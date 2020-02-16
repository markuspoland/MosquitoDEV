using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowObjectives : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
        textMesh.SetText(GameManager.Instance.ObjectivesCompleted.ToString() + " / 3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
