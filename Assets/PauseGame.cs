using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
    }
}
