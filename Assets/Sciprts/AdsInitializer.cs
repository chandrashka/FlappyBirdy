using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] bool _testMode = false;

    [SerializeField] InterstitialAdsButton interstitialAdsButton;
    [SerializeField] RewardedAdsButton rewardedAdsButton;

    void Awake()
    {
        InitializeAds();
        interstitialAdsButton.LoadAd();
        rewardedAdsButton.LoadAd();
    }
    public void InitializeAds()
    {
        Advertisement.Initialize(_androidGameId, _testMode, this);   
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        interstitialAdsButton.LoadAd();
    }
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
    }
}