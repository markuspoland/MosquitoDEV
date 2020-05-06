using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroMusicStart : MonoBehaviour
{
    float timer = 1f;
    AudioSource audioSource;
    VideoPlayer videoPlayer;
    double videoTime;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        videoPlayer = GetComponent<VideoPlayer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 3.8f * Time.deltaTime;

        videoTime += Time.deltaTime;

        if (timer <= 0)
        {
            audioSource.Play();
            timer = 300f;
        }

        if (videoTime >= videoPlayer.length + 0.5)
        {
            GameManager.Instance.ChangeScene(GameManager.GameScene.TheRoom);
        }

    }
}
