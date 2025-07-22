using System;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features
{
    [CreateAssetMenu(fileName = "GameModesToSymbolsConfig", menuName = "Configs/GameModesToSymbolsConfig")]
    public class GameModesToSymbolsConfig : ScriptableObject
    {
        [SerializeField] private GameModeSymbolsWrapper[] GameModesToSymbols = new GameModeSymbolsWrapper[Enum.GetNames(typeof(GameModes)).Length];

        public string GetSymbolsFrom(GameModes gameMode)
             => GameModesToSymbols.First(gameModeToSymbols => gameModeToSymbols.GameMode == gameMode).Symbols;
    }

    [Serializable]
    public class GameModeSymbolsWrapper
    {
        [field: SerializeField] public GameModes GameMode { get; private set; }
        [field: SerializeField] public string Symbols { get; private set; }
    }
}
