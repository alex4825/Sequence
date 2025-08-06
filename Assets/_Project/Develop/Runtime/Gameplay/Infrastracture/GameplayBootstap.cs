using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using System;
using UnityEngine;
using Assets._Project.Develop.Runtime.Infrastracture;
using Assets._Project.Develop.Runtime.Gameplay.Features;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastracture
{
    public class GameplayBootstap : SceneBootsprap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private GameplayCycle _gameplayCycle;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_container, gameplayInputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log($"Выбран режим {_inputArgs.GameMode}");

            Debug.Log("Инициализация геймплейной сцены.");

            _gameplayCycle = _container.Resolve<GameplayCycle>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт геймплейной сцены.");

            //_gameplayCycle.Launch();
        }

        private void Update()
        {
            _gameplayCycle?.Update();
        }        
    }
}
