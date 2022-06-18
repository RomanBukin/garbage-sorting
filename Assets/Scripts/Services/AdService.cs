using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;

namespace Services
{
    public class AdService : MonoBehaviour
    {
        [SerializeField] private string rewardedAdUnitId;
        [SerializeField] private string gameId;

        public EventHandler<RewardEventArgs> OnUserRewarded;

        private IRewardedAd _rewardedAd;

        private async void Start()
        {
            var initializationOptions = new InitializationOptions();
            initializationOptions.SetGameId(gameId);
            await UnityServices.InitializeAsync(initializationOptions);

            _rewardedAd = new RewardedAd(rewardedAdUnitId);

            _rewardedAd.OnLoaded += AdLoaded;
            _rewardedAd.OnFailedLoad += AdFailedToLoad;

            _rewardedAd.OnShowed += AdShown;
            _rewardedAd.OnFailedShow += AdFailedToShow;
            _rewardedAd.OnUserRewarded += UserRewarded;
            _rewardedAd.OnClosed += AdClosed;
            _rewardedAd.OnClicked += OnClicked;

            await _rewardedAd.LoadAsync();
        }

        void AdLoaded(object sender, EventArgs args)
        {
            Debug.Log("Ad loaded.");
            // Execute logic for when the ad has loaded
        }

        void AdFailedToLoad(object sender, LoadErrorEventArgs args)
        {
            Debug.Log("Ad failed to load.");
            // Execute logic for the ad failing to load.
        }

        private void OnClicked(object sender, EventArgs e)
        {
            print("On clicked");
        }
        void AdShown(object sender, EventArgs args)
        {
            Debug.Log("Ad shown successfully.");
            // Execute logic for the ad showing successfully.
        }

        void UserRewarded(object sender, RewardEventArgs args)
        {
            Debug.Log("Ad has rewarded user.");
            OnUserRewarded?.Invoke(sender, args);
        }

        void AdFailedToShow(object sender, ShowErrorEventArgs args)
        {
            Debug.Log("Ad failed to show.");
            // Execute logic for the ad failing to show.

            _rewardedAd.LoadAsync();
        }

        void AdClosed(object sender, EventArgs e)
        {
            Debug.Log("Ad is closed.");
            // Execute logic for the user closing the ad.

            _rewardedAd.LoadAsync();
        }

        public void ShowAd()
        {
            // Ensure the ad has loaded, then show it.
            if (_rewardedAd.AdState == AdState.Loaded)
            {
                _rewardedAd.ShowAsync();
            }
        }
    }
}