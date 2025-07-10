using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets.Develop.Runtime.Infrastracture.Gameplay.Utils;
using Assets.Develop.Runtime.Infrastracture.Gameplay.Views;
using System.Collections;
using UnityEngine;

namespace Assets.Develop.Runtime.Infrastracture.Gameplay.Mehanics
{
    public class GameplayCycle
    {
        private GameModes _gameModeType;
        private int _sequenceLenght;
        private GameplayView _gameplayView;
        private Popup _restartPopup;
        private DIContainer _container;

        private GameMode _gameMode;
        private SequenceGenerator _sequenceGenerator;

        public GameplayCycle(GameModes gameModeType, int sequenceLenght, GameplayView gameplayView, Popup restartPopup, DIContainer container)
        {
            _gameModeType = gameModeType;
            _sequenceLenght = sequenceLenght;
            _gameplayView = gameplayView;
            _restartPopup = restartPopup;
            _container = container;

            _sequenceGenerator = new SequenceGenerator(_container.Resolve<SymbolsGetter>().GetFrom(_gameModeType));
        }

        public void Launch()
        {
            string randomSequence = _sequenceGenerator.GetRandom(_sequenceLenght);

            _gameMode = new(randomSequence);
            _gameplayView.SetText(randomSequence);

            _gameMode.Win += OnGameModeWin;
            _gameMode.Defeat += OnGameModeDefeat;

            _gameMode.Start();
        }

        public void Update()
        {
            _gameMode?.Update();
        }

        private void OnGameModeDefeat()
        {
            Debug.Log("Defeat");
            _container.Resolve<ICoroutinesPerformer>().StartPerform(EndGame());
        }

        private void OnGameModeWin()
        {
            Debug.Log("Win");
            _container.Resolve<ICoroutinesPerformer>().StartPerform(EndGame());
        }

        private IEnumerator EndGame()
        {
            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;

            _restartPopup.SetText($"Press {KeyCode.F.ToString()} to restart or press {KeyCode.Escape.ToString()} to go to menu");

            _restartPopup.Show();

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape));

            if (Input.GetKeyDown(KeyCode.F))
                Launch();
            else if (Input.GetKeyDown(KeyCode.Escape))
                OpenMenu();

            _restartPopup.Hide();
        }

        private void OpenMenu()
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcesSwitchTo(Scenes.MainMenu));
        }
    }
}
