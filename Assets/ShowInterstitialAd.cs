using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInterstitialAd : MonoBehaviour
{
    [SerializeField] AdMobScript admob;
    void Start()
    {
        admob.RequestInterstitial();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
