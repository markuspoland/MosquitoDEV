using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;

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
        //GameManager.Instance.ChangeScene(GameManager.GameScene.TheRoom);
        StartCoroutine("LoadSceneAsync");
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("TheRoom");
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress);
            slider.value = progress;
            yield return null;
        }
    }
}
