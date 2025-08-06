using Assets._Project.Develop.Runtime.UI.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        public event Action GoToMenuButtonClicked;

        [SerializeField] private TMP_Text _sequenceText;
        [SerializeField] private TMP_Text _inputText;
        [SerializeField] private Button _goToMenuButton;

        public void SetSequenceText(string text) => _sequenceText.text = text;

        public void SetInputText(string text) => _inputText.text = text;

        private void OnEnable()
        {
            _goToMenuButton.onClick.AddListener(OnGoToMenuButtonClicked);
        }

        private void OnDisable()
        {
            _goToMenuButton.onClick.RemoveListener(OnGoToMenuButtonClicked);
        }

        private void OnGoToMenuButtonClicked()
            => GoToMenuButtonClicked?.Invoke();
    }
}
