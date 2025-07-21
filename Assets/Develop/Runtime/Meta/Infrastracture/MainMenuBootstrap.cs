using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets.Develop.Runtime.Infrastracture.Meta.Mehanics;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastracture.Meta.Infrastracture
{
    public class MainMenuBootstrap : SceneBootsprap
    {
        [SerializeField] private TMP_InputField _sequenceInputField;

        private DIContainer _container;
        private IGameModeSelector _gameModeSelector;

        private bool _isRunning;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }
        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация сцены главного меню.");

            _gameModeSelector = _container.Resolve<IGameModeSelector>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт сцены главного меню.");

            _isRunning = true;

            _container.Resolve<MetaToGameplayTransitor>().StartListen(_sequenceInputField, _gameModeSelector);
        }

        private void Update()
        {
            if (_isRunning)
            {
                _gameModeSelector?.Update();
            }
        }
    }
}
