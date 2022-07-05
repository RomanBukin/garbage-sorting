using Components;
using Game;
using Services;
using Unity.Services.Mediation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Controllers
{
    public class GameSelectMenuController : MonoBehaviour
    {
        [SerializeField] private bool locked;
        
        [SerializeField] private Toggle[] toggles;
        [SerializeField] private Transform[] colorPanels;
        [SerializeField] private Transform[] tanks;
        [SerializeField] private LockItem[] lockItems;

        [SerializeField] private Button backButton;
        [SerializeField] private Button startButton;

        private SceneSwitcher _sceneSwitcher;
        private AdService _adService;
        private CrossSceneContainer _crossSceneContainer;
        private GameMode _gameMode;

        private int _currentType;
        private int _difficultyRequest;

        [Inject]
        private void Construct(
            SceneSwitcher sceneSwitcher,
            AdService adService,
            CrossSceneContainer crossSceneContainer
        )
        {
            _sceneSwitcher = sceneSwitcher;
            _adService = adService;
            _crossSceneContainer = crossSceneContainer;
        }

        private void Awake()
        {
            _gameMode = new GameMode();

            SetUpEvents();
        }

        private void Start()
        {
#if UNITY_WEBGL
            locked = false;
#endif
            SetLocked(locked);

            _gameMode.SetDifficulty(0);
        }

        private void OnDestroy()
        {
            CleanUpEvents();
        }

        private void SetUpEvents()
        {
            for (int i = 0; i < 7; i++)
            {
                int index = i;
                toggles[index].onValueChanged.AddListener(value => OnToggleChanged(index, value));
            }

            backButton.onClick.AddListener(OnBackClicked);
            startButton.onClick.AddListener(OnStartClicked);

            _gameMode.TankEnableChanged += TankEnableChanged;
            _gameMode.MaxTankChanged += MaxTankChanged;

            _adService.OnUserRewarded += OnUserRewarded;
        }

        private void CleanUpEvents()
        {
            for (int i = 0; i < 7; i++)
            {
                int index = i;
                toggles[index].onValueChanged.RemoveListener(value => OnToggleChanged(index, value));
            }

            backButton.onClick.RemoveListener(OnBackClicked);
            startButton.onClick.RemoveListener(OnStartClicked);

            _gameMode.TankEnableChanged -= TankEnableChanged;
            _gameMode.MaxTankChanged -= MaxTankChanged;

            _adService.OnUserRewarded -= OnUserRewarded;
        }

        private void OnToggleChanged(int index, bool value)
        {
            if (!value)
            {
                return;
            }

            if (index >= 4)
            {
                if (locked)
                {
                    _adService.ShowAd();
                    _difficultyRequest = index;
                    toggles[0].isOn = true;
                    return;
                }
            }

            _gameMode.SetDifficulty(index);
        }

        private void OnBackClicked()
        {
            _sceneSwitcher.SwitchScene("Menu Scene");
        }

        private void OnStartClicked()
        {
            print($"Start, {_gameMode.GetTankTypes().Length}");
            _crossSceneContainer.Put(_gameMode);
            _sceneSwitcher.SwitchScene("Gameplay Scene");
        }

        private void TankEnableChanged(object sender, TankEnableEventArgs e)
        {
            tanks[e.Type].gameObject.SetActive(e.NewValue);
        }

        private void MaxTankChanged(object sender, MaxTankEventArgs e)
        {
            colorPanels[_currentType].gameObject.SetActive(false);
            colorPanels[e.Type].gameObject.SetActive(true);
            _currentType = e.Type;
        }

        private void OnUserRewarded(object sender, RewardEventArgs e)
        {
            SetLocked(false);
            toggles[_difficultyRequest].isOn = true;
        }

        private void SetLocked(bool value)
        {
            locked = value;

            foreach (var item in lockItems)
            {
                item.IsLocked = value;
            }
        }
    }
}