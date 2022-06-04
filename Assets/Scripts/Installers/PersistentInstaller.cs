using DG.Tweening;
using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PersistentInstaller : MonoInstaller, IInitializable
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
            DOTween.Init();
            Application.targetFrameRate = 60;
        }
    }
}