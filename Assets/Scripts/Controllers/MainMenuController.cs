using System;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        public Button playButton;
        public Toggle soundToggleButton;
        public Button recordsButton;
        public Button creditsButton;
        public Button quitButton;
        public Button closePopupButton;

        private AudioService _audioService;
        private SettingsService _settingsService;
        private RecordsService _recordsService;
        private SceneSwitcher _sceneSwitcher;
        private MainMenuAnimation _animation;

        [Inject]
        public void Construct(
            AudioService audioService,
            SettingsService settingsService,
            RecordsService recordsService,
            SceneSwitcher sceneSwitcher,
            MainMenuAnimation menuAnimation)
        {
            _audioService = audioService;
            _settingsService = settingsService;
            _recordsService = recordsService;
            _sceneSwitcher = sceneSwitcher;
            _animation = menuAnimation;

            _sceneSwitcher.FadeFinished += FadeFinished;
        }

        private void Start()
        {
            soundToggleButton.isOn = _settingsService.SoundEnabled;
            
            playButton.onClick.AddListener(OnClickPlayButton);
            soundToggleButton.onValueChanged.AddListener(OnChangedSoundToggle);
            recordsButton.onClick.AddListener(OnClickRecordsButton);
            creditsButton.onClick.AddListener(OnClickCreditsButton);
            quitButton.onClick.AddListener(Application.Quit);
            closePopupButton.onClick.AddListener(OnClickClosePopupButton);
        }

        private void OnDestroy()
        {
            _sceneSwitcher.FadeFinished -= FadeFinished;

            playButton.onClick.RemoveListener(OnClickPlayButton);
            soundToggleButton.onValueChanged.RemoveListener(OnChangedSoundToggle);
            recordsButton.onClick.RemoveListener(OnClickRecordsButton);
            creditsButton.onClick.RemoveListener(OnClickCreditsButton);
            quitButton.onClick.RemoveListener(Application.Quit);
            closePopupButton.onClick.RemoveListener(OnClickClosePopupButton);
        }

        private void OnClickClosePopupButton()
        {
            print("OnClickClosePopupButton");
            _animation.ShowIcons();
        }

        private void OnChangedSoundToggle(bool value)
        {
            print("OnChangedSoundToggle");
            if (_settingsService.SoundEnabled)
            {
                _audioService.StopMusic();
                _settingsService.SoundEnabled = false;
            }
            else
            {
                _audioService.PlayMenuMusic();
                _settingsService.SoundEnabled = true;
            }
        }

        private void OnClickCreditsButton()
        {
            print("OnClickCreditsButton");
            _animation.SelectCredits();
        }

        private void OnClickRecordsButton()
        {
            print("OnClickRecordsButton");
            _animation.SelectRecords();
        }

        private void OnClickPlayButton()
        {
            print("OnClickPlayButton");
            _sceneSwitcher.PrepareScene("Gameplay Scene");
            _animation.SelectPlay();
            _sceneSwitcher.SwitchToPreparedScene(0.5f);
        }

        private void FadeFinished(object sender, FadeEventArgs e)
        {
            if (e.TargetValue == 0f)
            {
                _animation.ShowIcons();
            }
        }
    }
}