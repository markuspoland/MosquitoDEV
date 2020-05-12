using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSoundManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip success;
    public AudioClip failure;
    public AudioClip hit;
    public AudioClip suck;

    public static AudioSource audioSource;
    public static AudioClip swipeSuccess;
    public static AudioClip swipeFailure;
    public static AudioClip _hit;
    public static AudioClip suckBlood;

    void Awake()
    {
        audioSource = source;
        swipeSuccess = success;
        swipeFailure = failure;
        _hit = hit;
        suckBlood = suck;
    }

}
