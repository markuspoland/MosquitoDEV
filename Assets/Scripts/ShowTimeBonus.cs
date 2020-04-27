using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowTimeBonus : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.SetText(GameManager.Instance.timeBonus.ToString());
    }
}
