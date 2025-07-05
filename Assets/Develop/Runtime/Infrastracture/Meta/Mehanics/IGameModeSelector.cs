using System;

namespace Assets.Develop.Runtime.Infrastracture.Meta.Mehanics
{
    public interface IGameModeSelector
    {
        event Action NumbersGameModeSelected;
        event Action LettersGameModeSelected;

        void Update();
    }
}
