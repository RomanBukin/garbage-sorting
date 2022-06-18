using Game;
using Zenject;

namespace Installers
{
    public class GameSelectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<GameMode>()
                .AsSingle();
        }
    }
}
