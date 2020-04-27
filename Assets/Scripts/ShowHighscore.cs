using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowHighscore : MonoBehaviour
{

    TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.SetText(GameManager.Instance.highscore.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
