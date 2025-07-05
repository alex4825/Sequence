using System;
using UnityEngine;

namespace Assets.Develop.Runtime.Infrastracture.Gameplay.Mehanics
{
    [CreateAssetMenu(fileName = "GameModesToSymbolsConfig", menuName = "Configs/GameModesToSymbolsConfig")]
    public class GameModesToSymbolsConfig : ScriptableObject
    {
        public GameModeSymbolsWrapper[] GameModesToSymbols = new GameModeSymbolsWrapper[Enum.GetNames(typeof(GameModes)).Length];
    }


    [Serializable]
    public class GameModeSymbolsWrapper
    {
        [field: SerializeField] public GameModes GameMode { get; private set; }
        [field: SerializeField] public string Symbols { get; private set; }
    }
}
