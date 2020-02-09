using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preload : MonoBehaviour
{

    float timer = 3f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            GameManager.Instance.ChangeScene(GameManager.GameScene.Intro);
        }
    }
}
