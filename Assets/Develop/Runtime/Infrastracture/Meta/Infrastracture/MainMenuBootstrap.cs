using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.Infrastracture.Gameplay.Infrastracture;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets.Develop.Runtime.Infrastracture.Gameplay.Mehanics;
using Assets.Develop.Runtime.Infrastracture.Meta.Mehanics;
using System;
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
            _gameModeSelector.GameModeSelected += OnGameModeSelected;

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт сцены главного меню.");

            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning)
            {
                _gameModeSelector?.Update();
            }
        }

        private void OnGameModeSelected(GameModes gameMode)
        {
            if (int.TryParse(_sequenceInputField.text, out int sequenceLength) && sequenceLength > 0)
            {
                _gameModeSelector.GameModeSelected -= OnGameModeSelected;
                StartGameplay(gameMode, sequenceLength);
            }
            else
            {
                Debug.Log("Enter positive int number greater than 0");
            }
        }

        private void StartGameplay(GameModes gameMode, int sequenceLength)
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcesSwitchTo(Scenes.Gameplay, new GameplayInputArgs(gameMode, sequenceLength)));
        }
    }
}
