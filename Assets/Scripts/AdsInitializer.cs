using UnityEngine;
using UnityEngine.Advertisements;

namespace Scripts
{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] private string androidGameId;
        [SerializeField] private bool testMode;

        [SerializeField] private InterstitialAdsButton interstitialAdsButton;
        [SerializeField] private RewardedAdsButton rewardedAdsButton;

        private void Awake()
        {
            InitializeAds();
            interstitialAdsButton.LoadAd();
            rewardedAdsButton.LoadAd();
        }

        private void InitializeAds()
        {
            Advertisement.Initialize(androidGameId, testMode, this);   
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
}