using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using System;
using UnityEngine;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets.Develop.Runtime.Infrastracture.Gameplay.Mehanics;
using System.Linq;
using Assets.Develop.Runtime.Infrastracture.Gameplay.Utils;
using Assets.Develop.Runtime.Infrastracture.Gameplay.Views;

namespace Assets._Project.Develop.Runtime.Infrastracture.Gameplay.Infrastracture
{
    public class GameplayBootstap : SceneBootsprap
    {
        [SerializeField] private Popup _restartPopup;
        [SerializeField] private GameplayView _gameplayView;

        private DIContainer _container;
        private GameplayInputArgs _inputArgs;

        private bool _isRunning;

        private string _symbols;
        private GameMode _gameMode;
        private SequenceGenerator _sequenceGenerator;

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

            _symbols = GetSymbolsFrom(_inputArgs.GameMode);
            _sequenceGenerator = new SequenceGenerator(_symbols);

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт геймплейной сцены.");

            _isRunning = true;

            StartNewGame();
        }

        private void Update()
        {
            if (_isRunning)
            {
                /*if (Input.GetKeyDown(KeyCode.Escape))
                    OpenMenu();*/

                _gameMode?.Update();
            }
        }

        private void StartNewGame()
        {
            string randomSequence = _sequenceGenerator.GetRandom(_inputArgs.SequenceLenght);

            _gameMode = new(randomSequence);
            _gameplayView.SetText(randomSequence);

            _gameMode.Win += OnGameModeWin;
            _gameMode.Defeat += OnGameModeDefeat;

            _gameMode.Start();
        }

        private void OnGameModeDefeat()
        {
            Debug.Log("Defeat");
            StartCoroutine(EndGame());
        }

        private void OnGameModeWin()
        {
            Debug.Log("Win");
            StartCoroutine(EndGame());
        }

        private IEnumerator EndGame()
        {
            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;

            _restartPopup.SetText($"Press {KeyCode.F.ToString()} to restart or press {KeyCode.Escape.ToString()} to go to menu");

            _restartPopup.Show();

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape));

            if (Input.GetKeyDown(KeyCode.F))
                StartNewGame();
            else if (Input.GetKeyDown(KeyCode.Escape))
                OpenMenu();

            _restartPopup.Hide();
        }

        private string GetSymbolsFrom(GameModes gameMode)
        {
            ConfigsProviderService configsProviderService = _container.Resolve<ConfigsProviderService>();
            GameModeSymbolsWrapper[] gameModesToSymbols = configsProviderService.GetConfig<GameModesToSymbolsConfig>().GameModesToSymbols;
            string symbols = gameModesToSymbols.First(gameModeToSymbols => gameModeToSymbols.GameMode == gameMode).Symbols;

            return symbols;
        }

        private void OpenMenu()
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcesSwitchTo(Scenes.MainMenu));
        }
    }
}
