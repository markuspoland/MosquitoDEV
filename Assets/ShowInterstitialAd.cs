using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowInterstitialAd : MonoBehaviour
{
    [SerializeField] AdMobScript admob;
    float timer;
    void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            admob.RequestInterstitial();
        }
        
        timer = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <=0 && !admob.IsInterstitialReady())
        {
            if (SceneManager.GetActiveScene().name != "LastADScene")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                GameManager.Instance.ChangeScene(GameManager.GameScene.Menu);
            }
        }
    }
}
