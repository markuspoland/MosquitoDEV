using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    
    [SerializeField] GameObject loadingScreen;

    void Start()
    {
        loadingScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        loadingScreen.SetActive(true);
        GameManager.Instance.ChangeScene(GameManager.GameScene.TheRoom);
    }
}
