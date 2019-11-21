using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSoundManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip success;
    public AudioClip failure;

    public static AudioSource audioSource;
    public static AudioClip swipeSuccess;
    public static AudioClip swipeFailure;

    void Awake()
    {
        audioSource = source;
        swipeSuccess = success;
        swipeFailure = failure;
    }

}
