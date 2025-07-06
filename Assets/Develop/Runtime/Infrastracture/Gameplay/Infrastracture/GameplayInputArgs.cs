using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets.Develop.Runtime.Infrastracture.Gameplay.Mehanics;

namespace Assets._Project.Develop.Runtime.Infrastracture.Gameplay.Infrastracture
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameModes gameMode, int sequenceLenght)
        {
            GameMode = gameMode;
            SequenceLenght = sequenceLenght;
        }

        public int SequenceLenght { get; }

        public GameModes GameMode { get; }
    }
}
