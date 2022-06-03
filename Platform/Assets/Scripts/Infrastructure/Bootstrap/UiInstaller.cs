using Infrastructure.UI;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class UiInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            BindHeroStatusMediator();

        private void BindHeroStatusMediator() =>
            Container.Bind<IUiHeroStatusMediator>().To<UiHeroStatusMediator>().AsSingle();
    }
}