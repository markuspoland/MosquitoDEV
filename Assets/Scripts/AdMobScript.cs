using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdMobScript : MonoBehaviour
{
    string App_ID = "ca-app-pub-4656290979684203~9381457693";

    string Banner_AD_ID = "ca-app-pub-4656290979684203/6340516919";
    string Interstitial_AD_ID = "ca-app-pub-4656290979684203/1212506836";
    string Video_AD_ID = "ca-app-pub-3940256099942544/5224354917";

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;

    bool bannerLoaded;
    void Start()
    {
        MobileAds.Initialize(App_ID);
        bannerLoaded = false;
        //MobileAds.Initialize(initStatus => { });


    }


    public void RequestBanner()
    {

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(Banner_AD_ID, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

        this.bannerView.OnAdLoaded += HandleOnBannerLoaded;
    }

    public void ShowBannerAd()
    {
        this.bannerView.Show();
    }

    public void HideBanner()
    {
        this.bannerView.Hide();
    }

    public void RequestInterstitial()
    {
        this.interstitial = new InterstitialAd(Interstitial_AD_ID);

        // Called when the ad is closed.
        this.interstitial.OnAdLoaded += HandleOnInterstitialAdLoaded;
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }

    }

    public void RequestRewardBasedVideo()
    {
        rewardBasedVideo = RewardBasedVideoAd.Instance;

        this.rewardBasedVideo.OnAdLoaded += HandleOnAdLoaded;
        this.rewardBasedVideo.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardBasedVideo.LoadAd(request, Video_AD_ID);
    }

    public void ShowVideoRewardAd()
    {
        if (this.rewardBasedVideo.IsLoaded())
        {
            this.rewardBasedVideo.Show();
        }

    }

    //       EVENTS    FOR    ADS      !!!!
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        ShowVideoRewardAd();
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");

        if (SceneManager.GetActiveScene().name != "LastADScene")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else
        {
            GameManager.Instance.ChangeScene(GameManager.GameScene.Menu);
        }
        
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void HandleOnInterstitialAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        ShowInterstitialAd();
    }

    public void HandleOnBannerLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        bannerLoaded = true;
    }

    private void OnDisable()
    {
        if (bannerLoaded)
        {
            bannerView.Hide();
        }
    }
}
