using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVideoAd : MonoBehaviour
{
    [SerializeField] AdMobScript admob;
    void Start()
    {
        admob.RequestRewardBasedVideo();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
