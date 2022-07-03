using Animation;
using Controllers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private GameObject animationController;
        [SerializeField] private GameObject mainController;
        
        public override void InstallBindings()
        {
            Container
                .Bind<MainMenuAnimation>()
                .FromComponentOn(animationController)
                .AsSingle();
            
            Container
                .Bind<MainMenuController>()
                .FromComponentOn(mainController)
                .AsSingle();
        }
    }
}