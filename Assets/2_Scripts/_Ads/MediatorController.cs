using System;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;

namespace Unity.Example
{
    public class MediatorController : MonoBehaviour, IDisposable
    {
        [SerializeField] private Event showAdEvent_;
        private void OnEnable()
        {
            showAdEvent_.callback += ShowAd;
        }
        private void OnDisable()
        {
            showAdEvent_.callback -= ShowAd;
        }

        [SerializeField] private Event adCompletedEvent;

        [Header("Fail Control")]
        // [SerializeField] private Event adInitFailEvent;
        // [SerializeField] private Event adLoadFailEvent;
        [SerializeField] private Event adShowFailEvent;

        IRewardedAd ad;
        [SerializeField] private string adUnitIdSuffix_Android = "_Android";
        [SerializeField] private string adUnitIdSuffix_iOS = "_iOS";
        string adUnitId = "Rewarded";
        [SerializeField] private string gameId_Android = "4626705";
        [SerializeField] private string gameId_iOS = "4626704";
        string gameId = "4626704";

        private void Start()
        {
            if(Application.platform == RuntimePlatform.Android)
            {
                gameId = gameId_Android;
                adUnitId += adUnitIdSuffix_Android;
            }
            else if(Application.platform == RuntimePlatform.Android)
            {
                gameId = gameId_iOS;
                adUnitId += adUnitIdSuffix_iOS;
            }
            InitServices();
        }

        private async Task InitServices()
        {
            try
            {
                InitializationOptions initializationOptions = new InitializationOptions();
                initializationOptions.SetGameId(gameId);
                await UnityServices.InitializeAsync(initializationOptions);
                InitializationComplete();
            }
            catch (Exception e)
            {
                InitializationFailed(e);

                await Task.Delay(1000);
                await InitServices();
            }
        }

        private void SetupAd()
        {
            //Create
            ad = MediationService.Instance.CreateRewardedAd(adUnitId);

            //Subscribe to events
            ad.OnClosed += AdClosed;
            ad.OnClicked += AdClicked;
            ad.OnLoaded += AdLoaded;
            ad.OnFailedLoad += AdFailedLoad;
            ad.OnUserRewarded += UserRewarded;

            // Impression Event
            MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
        }

        public void Dispose() => ad?.Dispose();

        private async void ShowAd()
        {
            if (!GameData.isAdLoaded.value) adShowFailEvent.Invoke();
            else
                try
                {
                    RewardedAdShowOptions showOptions = new RewardedAdShowOptions();
                    showOptions.AutoReload = true;
                    await ad.ShowAsync(showOptions);
                    AdShown();
                }
                catch (ShowFailedException e)
                {
                    AdFailedShow(e);
                }
        }

        private void InitializationComplete()
        {
            GameData.isAdInitialized.value = true;
            SetupAd();
            LoadAd();
        }

        async Task LoadAd()
        {
            try
            {
                await ad.LoadAsync();
            }
            catch (LoadFailedException)
            {
                await Task.Delay(1000);
                await LoadAd();
            }
        }

        void InitializationFailed(Exception e)
        {
            GameData.isAdInitialized.value = false;
            Debug.Log("Failed to Initialize ad");
        }

        void AdLoaded(object sender, EventArgs e)
        {
            GameData.isAdLoaded.value = true;
            Debug.Log("Ad loaded YEAH~");
        }

        void AdFailedLoad(object sender, LoadErrorEventArgs e)
        {
            GameData.isAdLoaded.value = false;
            Debug.Log("Failed to load ad");
            Debug.Log(e.Message);
        }

        void AdShown()
        {
            Debug.Log("Ad shown!");
        }

        void AdClosed(object sender, EventArgs e)
        {
            GameData.isAdLoaded.value = ad.AdState == AdState.Loaded;
            Debug.Log("AD CLOSED : " + GameData.isAdInitialized.value);
        }

        void AdClicked(object sender, EventArgs e)
        {
            Debug.Log("Ad has been clicked");
        }

        void AdFailedShow(ShowFailedException e)
        {
            Debug.Log(e.Message);
        }

        void ImpressionEvent(object sender, ImpressionEventArgs args)
        {
            var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
            Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
        }

        void UserRewarded(object sender, RewardEventArgs e)
        {
            Debug.Log($"Received reward: type:{e.Type}; amount:{e.Amount}");

            adCompletedEvent.Invoke();
        }
    }
}