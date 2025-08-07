using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features
{
    public class GameplayCycle : IDisposable
    {
        public event Action GameWin;
        public event Action GameDefeat;

        private int _sequenceLenght;

        private GameMode _gameMode;
        private SequenceGenerator _sequenceGenerator;

        private ReactiveVariable<string> _inputText = new(string.Empty);
        private ReactiveVariable<string> _sequence = new(string.Empty);

        public GameplayCycle(
            int sequenceLenght, 
            SequenceGenerator sequenceGenerator)
        {
            _sequenceLenght = sequenceLenght;
            _sequenceGenerator = sequenceGenerator;
        }

        public IReadonlyVariable<string> InputText => _inputText;

        public IReadonlyVariable<string> Sequence => _sequence;

        public void Launch()
        {
            string randomSequence = _sequenceGenerator.GetRandom(_sequenceLenght);

            _gameMode = new(randomSequence);
            _sequence.Value = randomSequence;

            _gameMode.Win += OnGameModeWin;
            _gameMode.Defeat += OnGameModeDefeat;
            _gameMode.SymbolEntered += OnSymbolEntered;

            _gameMode.Start();
        }

        public void Update()
        {
            _gameMode?.Update();
        }

        private void OnSymbolEntered(char symbol)
        {
            _inputText.Value += symbol;
        }

        private void OnGameModeDefeat()
        {
            Debug.Log("Defeat");

            EndGame();
            GameDefeat?.Invoke();
        }

        private void OnGameModeWin()
        {
            Debug.Log("Win");

            EndGame();
            GameWin?.Invoke();
        }

        private void EndGame()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_gameMode == null)
                return;

            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;
            _gameMode.SymbolEntered -= OnSymbolEntered;

            _gameMode = null;

            _inputText.Value = string.Empty;
        }
    }
}
