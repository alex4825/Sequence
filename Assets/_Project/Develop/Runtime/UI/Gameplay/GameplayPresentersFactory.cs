using Assets._Project.Develop.Runtime.Configs.Meta;
using Assets._Project.Develop.Runtime.Gameplay.Features;
using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.Infrastracture.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayPresentersFactory
    {
        private readonly DIContainer _container;

        public GameplayPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public GameplayPresenter CreateGameplayPresenter(GameplayScreenView view)
        {
            return new GameplayPresenter(
                view,
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<ConfigsProviderService>().GetConfig<CostsConfig>(),
                _container.Resolve<GameplayCycle>(),
                _container.Resolve<WalletService>(),
                _container.Resolve<WinDefeatCounter>()
                );
        }
    }
}
