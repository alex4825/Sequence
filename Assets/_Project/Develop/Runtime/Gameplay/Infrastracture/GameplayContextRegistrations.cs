using Assets._Project.Develop.Runtime.Gameplay.Features;
using Assets._Project.Develop.Runtime.Infrastracture.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastracture
{
    public class GameplayContextRegistrations
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Процесс регистрации сервисов на сцене геймплея");

            container.RegisterAsSingle(CreateSymbolsGetter);
        }

        private static SymbolsGetter CreateSymbolsGetter(DIContainer container)
            => new SymbolsGetter(container);
    }
}
