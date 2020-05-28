using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
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
            GameManager.Instance.isPaused = true;
            settingsPanel.SetActive(true);
            Time.timeScale = 0;
        } else
        {
            GameManager.Instance.isPaused = false;
            Time.timeScale = 1;
            settingsPanel.SetActive(false);
        }
    }
}
