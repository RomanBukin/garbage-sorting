using DG.Tweening;
using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private GameObject audioService;
        [SerializeField] private GameObject settingsService;
        [SerializeField] private GameObject recordsService;
        [SerializeField] private GameObject sceneSwitcher;

        public override void InstallBindings()
        {
            BindAudioService();
            BindSettingsService();
            BindRecordsService();
            BindSceneSwitcher();
            StartInit();
        }

        private void BindAudioService()
        {
            Container
                .Bind<AudioService>()
                .FromComponentInNewPrefab(audioService)
                .AsSingle()
                .NonLazy();
        }

        private void BindSettingsService()
        {
            Container
                .Bind<SettingsService>()
                .FromComponentInNewPrefab(settingsService)
                .AsSingle()
                .NonLazy();
        }

        private void BindRecordsService()
        {
            Container
                .Bind<RecordsService>()
                .FromComponentInNewPrefab(recordsService)
                .AsSingle()
                .NonLazy();
        }

        private void BindSceneSwitcher()
        {
            Container
                .Bind<SceneSwitcher>()
                .FromComponentInNewPrefab(sceneSwitcher)
                .AsSingle()
                .NonLazy();
        }

        public void Initialize()
        {
            Container.InstantiateComponentOnNewGameObject<StandaloneInputModule>("Event System");
            
            DOTween.Init();
            Application.targetFrameRate = 60;
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