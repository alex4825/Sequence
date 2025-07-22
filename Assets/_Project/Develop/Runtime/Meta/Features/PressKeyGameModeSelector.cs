using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features
{
    public class PressKeyGameModeSelector : IGameModeSelector
    {
        public event Action<GameModes> GameModeSelected;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                GameModeSelected?.Invoke(GameModes.Numbers);
            else if (Input.GetKeyDown(KeyCode.W))
                GameModeSelected?.Invoke(GameModes.Letters);
        }
    }
}
