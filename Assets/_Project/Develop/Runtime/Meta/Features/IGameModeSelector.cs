using System;

namespace Assets._Project.Develop.Runtime.Meta.Features
{
    public interface IGameModeSelector
    {
        event Action<GameModes> GameModeSelected;

        void Update();
    }
}
