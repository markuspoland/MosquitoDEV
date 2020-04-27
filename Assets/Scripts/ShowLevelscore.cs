using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ShowLevelscore : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    string currentLevel;
    private void OnEnable()
    {
        currentLevel = SceneManager.GetActiveScene().name;
        textMesh = GetComponent<TextMeshProUGUI>();

        if (currentLevel == "TheRoom")
        {
            textMesh.SetText(GameManager.Instance.level1Points.ToString());
        }
    }
}
