using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("DisableMe", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisableMe()
    {
        gameObject.SetActive(false);
    }
}
