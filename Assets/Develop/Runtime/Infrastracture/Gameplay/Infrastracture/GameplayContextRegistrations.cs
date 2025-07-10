using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets.Develop.Runtime.Infrastracture.Gameplay.Mehanics;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastracture.Gameplay.Infrastracture
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
