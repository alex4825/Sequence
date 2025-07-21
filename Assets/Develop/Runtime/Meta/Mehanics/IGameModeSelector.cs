using Assets.Develop.Runtime.Infrastracture.Gameplay.Mehanics;
using System;

namespace Assets.Develop.Runtime.Infrastracture.Meta.Mehanics
{
    public interface IGameModeSelector
    {
        event Action<GameModes> GameModeSelected;

        void Update();
    }
}
