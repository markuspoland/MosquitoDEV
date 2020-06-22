using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ShowInterstitialAd : MonoBehaviour
{
    [SerializeField] AdMobScript admob;
    float timer;
    void Start()
    {
        StartCoroutine(checkInternetConnection((isConnected) => {
            if (isConnected)
            {
                admob.RequestInterstitial();
            }

            if (!isConnected)
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
        }));

        if (Application.platform == RuntimePlatform.WindowsEditor)
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

    IEnumerator checkInternetConnection(Action<bool> action)
    {
        UnityWebRequest www = new UnityWebRequest("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }
}
