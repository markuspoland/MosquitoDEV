using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preload : MonoBehaviour
{

    float timer = 3f;
    [SerializeField] AdMobScript admob;
    void Start()
    {
        admob.RequestBanner();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            GameManager.Instance.ChangeScene(GameManager.GameScene.Menu);
        }
    }

    
}
