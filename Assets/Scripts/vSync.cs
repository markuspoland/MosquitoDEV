using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vSync : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
