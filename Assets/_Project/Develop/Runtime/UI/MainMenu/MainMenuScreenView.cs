using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action<GameModes, string> StartGameplayButtonClicked;
        public event Action ResetGameButtonClicked;

        [SerializeField] private Button _startLettersModeButton;
        [SerializeField] private Button _startNumbersModeButton;
        [SerializeField] private Button _resetGameButton;
        [SerializeField] private TMP_InputField _sequenceLengthInputField;

        [field: SerializeField] public IconTextListView WalletView { get; private set; }

        private void OnEnable()
        {
            _startLettersModeButton.onClick.AddListener(OnStartLettersModeButtonClicked);
            _startNumbersModeButton.onClick.AddListener(OnStartNumbersModeButtonClicked);
            _resetGameButton.onClick.AddListener(OnResetButtonClicked);
        }

        private void OnDisable()
        {
            _startLettersModeButton.onClick.RemoveListener(OnStartLettersModeButtonClicked);
            _startNumbersModeButton.onClick.RemoveListener(OnStartNumbersModeButtonClicked);
            _resetGameButton.onClick.RemoveListener(OnResetButtonClicked);
        }

        private void OnStartLettersModeButtonClicked()
            => StartGameplayButtonClicked?.Invoke(GameModes.Letters, _sequenceLengthInputField.text);

        private void OnStartNumbersModeButtonClicked()
            => StartGameplayButtonClicked?.Invoke(GameModes.Numbers, _sequenceLengthInputField.text);

        private void OnResetButtonClicked() => ResetGameButtonClicked?.Invoke();
    }
}
