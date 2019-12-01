using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Displays / Hides ads
/// </summary>
public class Monetizer : MonoBehaviour
{
    public bool timedBanner;
    public float bannerDuration;

	
	void Start ()
    {
        AdsCtrl.instance.ShowBanner();
	}
	
	void OnDisable()
    {
        if (!timedBanner)
            AdsCtrl.instance.HideBanner();
        else
            AdsCtrl.instance.HideBanner(bannerDuration);
    }
}
