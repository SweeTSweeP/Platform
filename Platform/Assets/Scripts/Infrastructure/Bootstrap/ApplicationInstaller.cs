using Crystal;
using Infrastructure.SceneManagement;
using Infrastructure.States;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class ApplicationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindCrystalSpawner();
            BindSceneLoader();
            BindStateMachine();
        }

        private void BindStateMachine() => 
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();

        private void BindSceneLoader() =>
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

        private void BindCrystalSpawner() =>
            Container.Bind<ICrystalSpawner>().To<CrystalSpawner>().AsSingle();
    }
}