using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Features
{
    public class GameMode
    {
        public event Action Win;
        public event Action Defeat;
        public event Action<char> SymbolEntered;

        private string _sequence;
        private int _currentIndex;

        private bool _isRunning;

        private KeysListener _keysListener = new();

        public GameMode(string sequence)
        {
            _sequence = sequence;
        }

        public void Start()
        {
            _isRunning = true;
            _currentIndex = 0;
            _keysListener.KeyPressed += OnKeyPressed;
        }

        public void Update()
        {
            if (_isRunning)
            {
                _keysListener?.Update();
            }
        }

        private void OnKeyPressed(char key)
        {
            if (char.ToLower(key) == char.ToLower(_sequence[_currentIndex]))
            {
                SymbolEntered?.Invoke(key);
                _currentIndex++;
            }
            else
            {
                Defeat?.Invoke();
                End();
            }

            if (_currentIndex >= _sequence.Length)
            {
                Win?.Invoke();
                End();
            }
        }

        private void End()
        {
            _keysListener.KeyPressed -= OnKeyPressed;
        }
    }
}
