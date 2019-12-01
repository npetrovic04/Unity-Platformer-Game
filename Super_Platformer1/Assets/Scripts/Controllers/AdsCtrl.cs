using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsCtrl : MonoBehaviour
{
    public static AdsCtrl instance = null;
    public string Android_Admob_Banner_ID;          //ca-app-pub-3940256099942544/6300978111 -> this is Test Ad
    public string Android_Admob_Interstitial_ID;    //ca-app-pub-3940256099942544/1033173712

    public bool testMode;
    BannerView bannerView;              // container for Banner ad
    InterstitialAd interstitial;
    AdRequest request;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RequestBanner()
    {
        // Create a 320x50 banner at the top of the screen
        if (testMode)
        {
            //         bannerView = new BannerView(Android_Admob_Banner_ID, AdSize.Banner, AdPosition.Top);        // this is a test number and works when Test Mode is checked in Unity
        }
        else
        {
              bannerView = new BannerView(Android_Admob_Banner_ID, AdSize.Banner, AdPosition.Top);      // this is the part for Live AD, and it is necessary to comment on the above line and make sure to uncheck the unity test in unity and also enter the number for live ad in unity
        }

        // Create an empty ad request
        AdRequest adRequest = new AdRequest.Builder().Build();

        // Load banner with request
        bannerView.LoadAd(adRequest);
    }

    public void ShowBanner()
    {
        bannerView.Show();
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }

    public void HideBanner(float duration)
    {
        StartCoroutine("HideBannerRoutine", duration);
    }

    IEnumerator HideBannerRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        bannerView.Hide();
    }

    void RequestInterstitial()
    {
        // inicijalizuje interstitial ads
        if (testMode)
        {
       //     interstitial = new InterstitialAd(Android_Admob_Interstitial_ID);
        }
        else
        {
              interstitial = new InterstitialAd(Android_Admob_Interstitial_ID);     // when a live ad number is drawn, enter it in unity and run a mode test
        }

        // Create an empty ad request
        request = new AdRequest.Builder().Build();

        // Load banner with request
        interstitial.LoadAd(request);

        interstitial.OnAdClosed += HandleOnAdClosed;
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        interstitial.Destroy();
        RequestInterstitial();
    }

    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    void OnEnable()
    {
        RequestBanner();

        RequestInterstitial();
    }

    void OnDisable()
    {
        bannerView.Destroy();

        interstitial.Destroy();
    }
}
