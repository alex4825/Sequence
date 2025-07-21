using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets.Develop.Runtime.Infrastracture.Meta.Mehanics;
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
        }

        private static PressKeyGameModeSelector CreatePressKeyGameModeSelector(DIContainer container)
            => new PressKeyGameModeSelector();
        
        private static MetaToGameplayTransitor CreateMetaToGameplayTransitor(DIContainer container)
            => new MetaToGameplayTransitor(container);
    }
}
