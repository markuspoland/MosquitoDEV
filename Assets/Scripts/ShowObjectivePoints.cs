using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowObjectivePoints : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    private void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.SetText(GameManager.Instance.objectivePoints.ToString());
    }
}
