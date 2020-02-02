using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowBonus : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.SetText(GameManager.Instance.levelCoinPoints.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
