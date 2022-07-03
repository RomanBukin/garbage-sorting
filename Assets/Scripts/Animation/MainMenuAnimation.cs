using Animation.Components;
using UnityEngine;

namespace Animation
{
    public class MainMenuAnimation : MonoBehaviour
    {
        private enum State
        {
            Hidden,
            Default,
            Credits,
            Records
        }

        public FadeAnimation fadePanel;
        public PlayButtonAnimation playIcon;
        public CornerIconAnimation soundIcon;
        public CornerIconAnimation creditsIcon;
        public CornerIconAnimation recordsIcon;
        public CornerIconAnimation quitIcon;
        public MenuPanelAnimation menuPanel;

        [SerializeField] private GameObject[] creditsVisibleElements; 
        [SerializeField] private GameObject[] recordsVisibleElements; 

        private State _state = State.Hidden;

        public void ShowIcons()
        {
            print("Show Icons");
            switch(_state)
            {
                case State.Default:
                    return;
                case State.Hidden:
                    creditsIcon.MoveToDefault();
                    recordsIcon.MoveToDefault();
                    fadePanel.FadeIn();
                    playIcon.FadeIn();
                    playIcon.StartPulsation();
                    break;
                case State.Credits:
                    Deselect(creditsIcon);
                    recordsIcon.MoveToDefault();
                    break;
                case State.Records:
                    Deselect(recordsIcon);
                    creditsIcon.MoveToDefault();
                    break;
            }

            _state = State.Default;

            soundIcon.MoveToDefault();
            quitIcon.MoveToDefault();
        }

        private void Deselect(CornerIconAnimation iconAnimation)
        {
            print("Deselect");
            menuPanel.Hide();
            
            iconAnimation.FadeIn(() =>
            {
                print("Deselect Callback");
                iconAnimation.MoveToDefault();
                fadePanel.FadeIn();
                playIcon.FadeIn();
                playIcon.StartPulsation();
            });
        }

        public void SelectCredits()
        {
            if (_state != State.Default)
            {
                return;
            }
            _state = State.Credits;

            foreach (var element in creditsVisibleElements)
            {
                element.SetActive(true);
            }
            
            foreach (var element in recordsVisibleElements)
            {
                element.SetActive(false);
            }
            
            creditsIcon.MoveToCenter(() =>
            {
                creditsIcon.FadeOut();
                menuPanel.Show(Vector2.one * 0.5f);
            });
            
            soundIcon.MoveOut();
            recordsIcon.MoveOut();
            quitIcon.MoveOut();
            
            fadePanel.FadeOut();
            playIcon.FadeOut();
            playIcon.PausePulsation();
        }
        
        public void SelectRecords()
        {
            if (_state != State.Default)
            {
                return;
            }
            _state = State.Records;

            foreach (var element in creditsVisibleElements)
            {
                element.SetActive(false);
            }
            
            foreach (var element in recordsVisibleElements)
            {
                element.SetActive(true);
            }
            
            print("SelectRecords");
            recordsIcon.MoveToCenter(() =>
            {
                print("Callback SelectRecords");
                recordsIcon.FadeOut();
                menuPanel.Show(Vector2.one * 0.5f);
            });
            
            soundIcon.MoveOut();
            creditsIcon.MoveOut();
            quitIcon.MoveOut();
            
            fadePanel.FadeOut();
            playIcon.FadeOut();
            playIcon.PausePulsation();
        }

        public void SelectPlay()
        {
            if (_state != State.Default)
            {
                return;
            }
            _state = State.Hidden;
            
            fadePanel.FadeOut();
            playIcon.FadeOut();
            playIcon.PausePulsation();
            
            soundIcon.MoveOut();
            creditsIcon.MoveOut();
            recordsIcon.MoveOut();
            quitIcon.MoveOut();
        }
    }
}
