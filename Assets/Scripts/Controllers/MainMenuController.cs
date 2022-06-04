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
        
        private MainMenuAnimation _animation;
        private SceneSwitcher _sceneSwitcher;
        private AudioService _audioService;

        [Inject]
        public void Construct(
            AudioService audioService,
            SceneSwitcher sceneSwitcher,
            MainMenuAnimation menuAnimation
            )
        {
            _audioService = audioService;
            _sceneSwitcher = sceneSwitcher;
            _animation = menuAnimation;
            
            _sceneSwitcher.FadeFinished += FadeFinished;
        }

        private void Start()
        {
            // _audioService.PlayMenuMusic();
            
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
            // if (_audioService.IsPlaying)
            // {
                // _audioService.Stop();
            // }
            // else
            // {
                // _audioService.PlayMenuMusic();
            // }
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
