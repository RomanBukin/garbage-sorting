using DG.Tweening;
using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private GameObject audioServicePrefab;
        [SerializeField] private GameObject settingsServicePrefab;
        [SerializeField] private GameObject recordsServicePrefab;
        [SerializeField] private GameObject sceneSwitcherPrefab;
        [SerializeField] private GameObject adServicePrefab;

        public override void InstallBindings()
        {
            BindAudioService();
            BindSettingsService();
            BindRecordsService();
            BindSceneSwitcher();
            BindAdService();
            
            StartInit();
        }

        private void BindAudioService()
        {
            Container
                .Bind<AudioService>()
                .FromComponentInNewPrefab(audioServicePrefab)
                .AsSingle()
                .NonLazy();
        }

        private void BindSettingsService()
        {
            Container
                .Bind<SettingsService>()
                .FromComponentInNewPrefab(settingsServicePrefab)
                .AsSingle()
                .NonLazy();
        }

        private void BindRecordsService()
        {
            Container
                .Bind<RecordsService>()
                .FromComponentInNewPrefab(recordsServicePrefab)
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneSwitcher()
        {
            Container
                .Bind<SceneSwitcher>()
                .FromComponentInNewPrefab(sceneSwitcherPrefab)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindAdService()
        {
            Container
                .Bind<AdService>()
                .FromComponentInNewPrefab(adServicePrefab)
                .AsSingle()
                .NonLazy();
        }

        public void Initialize()
        {
            Container.InstantiateComponentOnNewGameObject<StandaloneInputModule>("Event System");

            DOTween.Init();
            DOTween.defaultAutoPlay = AutoPlay.AutoPlayTweeners;
            Application.targetFrameRate = 60;

            var audioService = Container.Resolve<AudioService>();
            var settingsService = Container.Resolve<SettingsService>();

            if (settingsService.SoundEnabled)
            {
                audioService.PlayMenuMusic();
            }
        }

        private void StartInit()
        {
            Container
                .Bind<IInitializable>()
                .FromInstance(this)
                .AsSingle();
        }
    }
}