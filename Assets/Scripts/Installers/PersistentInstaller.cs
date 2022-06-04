using Services;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PersistentInstaller : MonoInstaller
    {
        [SerializeField] private GameObject audioService;
        [SerializeField] private GameObject settingsService;
        [SerializeField] private GameObject recordsService;
        
        public override void InstallBindings()
        {
            BindAudioService();
            BindSettingsService();
            BindRecordsService();
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
    }
}