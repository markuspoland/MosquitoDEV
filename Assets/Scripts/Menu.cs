using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] AudioSource musicSource;

    AudioSource audioSource;
    [SerializeField] AudioClip selectClip;

    [SerializeField] Animator levelSelectAnimator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        loadingScreen.SetActive(false);
        GameManager.Instance.LoadScore();
        scoreText.SetText(GameManager.Instance.highscore.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        loadingScreen.SetActive(true);
        audioSource.PlayOneShot(selectClip);
        StartCoroutine(FadeOut(musicSource, 0.2f));
        StartCoroutine(LoadSceneAsync("Guide"));
    }

    public void ShowLevelSelect()
    {
        audioSource.PlayOneShot(selectClip);
        levelSelectAnimator.SetTrigger("ShowLevelSelect");
    }

    public void HideLevelSelect()
    {
        audioSource.PlayOneShot(selectClip);
        levelSelectAnimator.SetTrigger("HideLevelSelect");
    }

    public void Records()
    {
        audioSource.PlayOneShot(selectClip);
    }

    public void Leaderboard()
    {
        audioSource.PlayOneShot(selectClip);
        GameManager.Instance.OpenLeaderboard();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadLevel1()
    {
        audioSource.PlayOneShot(selectClip);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync("TheRoom"));
    }


    IEnumerator LoadSceneAsync(string level)
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress);
            slider.value = progress;
            yield return null;
        }
    }

    IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

}
