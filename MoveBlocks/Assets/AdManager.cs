using UnityEngine;
using System.Collections;
using GoogleMobileAds.Common;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RequestBanner();
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    // creates banner
    // link to docs: https://firebase.google.com/docs/admob/unity/start
    private void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "INSERT_ANDROID_BANNER_AD_UNIT_ID_HERE";
#elif UNITY_IPHONE
        string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }
}
