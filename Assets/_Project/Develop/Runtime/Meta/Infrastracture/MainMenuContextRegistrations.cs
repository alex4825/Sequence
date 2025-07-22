using Assets._Project.Develop.Runtime.Configs.Meta;
using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.Infrastracture.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastracture.Meta.Infrastracture
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Процесс регистрации сервисов на сцене главного меню");

            container.RegisterAsSingle<IGameModeSelector>(CreatePressKeyGameModeSelector);

            container.RegisterAsSingle(CreateMetaToGameplayTransitor);

            container.RegisterAsSingle(CreateGameResetter);
        }

        private static GameResetter CreateGameResetter(DIContainer container)
            => new GameResetter(
                container.Resolve<WalletService>(),
                container.Resolve<VictoryDefeatCounter>(),
                container.Resolve<PlayerDataProvider>(),
                container.Resolve<ConfigsProviderService>().GetConfig<CostsConfig>().GameResetCost
                );

        private static PressKeyGameModeSelector CreatePressKeyGameModeSelector(DIContainer container)
            => new PressKeyGameModeSelector();
        
        private static MetaToGameplayTransitor CreateMetaToGameplayTransitor(DIContainer container)
            => new MetaToGameplayTransitor(container);
    }
}
