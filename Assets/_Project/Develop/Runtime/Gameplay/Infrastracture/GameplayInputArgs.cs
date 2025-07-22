using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastracture
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
