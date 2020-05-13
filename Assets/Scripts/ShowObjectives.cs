using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShowObjectives : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    LevelManager levelManager;
    string objectiveCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();

        if (SceneManager.GetActiveScene().name == "TheRoom")
        {
            objectiveCount = "4";
        }

        textMesh.SetText(GameManager.Instance.ObjectivesCompleted.ToString() + " / " + objectiveCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
