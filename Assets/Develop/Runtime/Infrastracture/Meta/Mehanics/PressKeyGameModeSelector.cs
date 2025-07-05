using System;
using UnityEngine;

namespace Assets.Develop.Runtime.Infrastracture.Meta.Mehanics
{
    public class PressKeyGameModeSelector : IGameModeSelector
    {
        public event Action NumbersGameModeSelected;
        public event Action LettersGameModeSelected;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                NumbersGameModeSelected?.Invoke();
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                LettersGameModeSelected?.Invoke();
        }
    }
}
