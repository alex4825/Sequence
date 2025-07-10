using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.Infrastracture.Gameplay.Infrastracture;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets.Develop.Runtime.Infrastracture.Gameplay.Mehanics;
using TMPro;
using UnityEngine;

namespace Assets.Develop.Runtime.Infrastracture.Meta.Mehanics
{
    public class MetaToGameplayTransitor
    {
        private DIContainer _container;
        private TMP_InputField _sequeceInputField;
        private IGameModeSelector _gameModeSelector;

        public MetaToGameplayTransitor(DIContainer container)
        {
            _container = container;
        }

        public void StartListen(TMP_InputField sequeceInputField, IGameModeSelector gameModeSelector) 
        {
            _sequeceInputField = sequeceInputField;
            _gameModeSelector = gameModeSelector;

            _gameModeSelector.GameModeSelected += OnGameModeSelected;
        }

        private void OnGameModeSelected(GameModes gameMode)
        {
            if (int.TryParse(_sequeceInputField.text, out int sequenceLength) && sequenceLength > 0)
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
