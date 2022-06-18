using Game;
using Game.Garbage;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public Transform spawnPosition;
        public int difficulty = 0;
        
        public override void InstallBindings()
        {
            InitDevelop();
            
            BindGameplay();
            BindGarbageFactory();
        }

        private void InitDevelop()
        {
            var container = new CrossSceneContainer();
            var gameMode = new GameMode();
            gameMode.SetDifficulty(difficulty);
            container.Put(gameMode);
            Container.BindInstance(container);
        }

        private void BindGameplay()
        {
            Container
                .Bind<Gameplay>()
                .AsSingle();
        }

        private void BindGarbageFactory()
        {
            Container
                .Bind<GarbageFactory>()
                .AsSingle()
                .WithArguments(spawnPosition.position);
        }
    }
}