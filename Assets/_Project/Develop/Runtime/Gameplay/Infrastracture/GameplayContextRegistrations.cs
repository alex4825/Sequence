using Assets._Project.Develop.Runtime.Gameplay.Features;
using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Gameplay;
using Assets._Project.Develop.Runtime.UI.MainMenu;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastracture
{
    public class GameplayContextRegistrations
    {
        static private GameplayInputArgs _args;

        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Процесс регистрации сервисов на сцене геймплея");

            _args = args;

            container.RegisterAsSingle(CreateSymbolsGetter);

            container.RegisterAsSingle(CreateGameplayUIRoot).NonLazy();

            container.RegisterAsSingle(CreateGameplayCycle);

            container.RegisterAsSingle(CreateGameplayPresentersFactory);

            container.RegisterAsSingle(CreateGameplayPresenter).NonLazy();
        }

        private static GameplayPresenter CreateGameplayPresenter(DIContainer container)
        {
            UIRoot uiRoot = container.Resolve<UIRoot>();
            GameplayScreenView view = container.Resolve<ViewsFactory>().Create<GameplayScreenView>(ViewIDs.GameplayScreen, uiRoot.HUDLayer);

            GameplayPresenter presenter = container.Resolve<GameplayPresentersFactory>().CreateGameplayPresenter(view);

            return presenter;
        }

        private static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer container)
            => new GameplayPresentersFactory(container);

        private static GameplayCycle CreateGameplayCycle(DIContainer container)
        {
            string symbols = container.Resolve<SymbolsGetter>().GetFrom(_args.GameMode);

            return new GameplayCycle(
                _args.SequenceLenght,
                new SequenceGenerator(symbols));
        }

        private static UIRoot CreateGameplayUIRoot(DIContainer container)
        {
            return container.Resolve<ViewsFactory>().Create<UIRoot>(ViewIDs.UIRoot);
        }

        private static SymbolsGetter CreateSymbolsGetter(DIContainer container)
            => new SymbolsGetter(container);
    }
}
