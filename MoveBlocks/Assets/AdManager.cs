using UnityEngine;
using System.Collections;
using GoogleMobileAds.Common;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {

    BannerView bannerView;

    float time = 0;

    // Use this for initialization
    void Start () {
        RequestBanner();
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(time > 60)
        {
            RequestBanner();
            bannerView.Hide();
        }   
	}


    // creates banner
    // link to docs: https://firebase.google.com/docs/admob/unity/start
    private void RequestBanner()
    {
        bannerView.OnAdLoaded -= BannerView_OnAdLoaded;

#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-2780744705157681/9009802704";
#elif UNITY_IPHONE
        string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView  = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);

        bannerView.OnAdLoaded += BannerView_OnAdLoaded;
    }

    private void BannerView_OnAdLoaded(object sender, System.EventArgs e)
    {
        bannerView.Show();
    }
}
