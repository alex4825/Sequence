using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features
{
    public class SequenceGenerator
    {
        private string _symbols;

        public SequenceGenerator(string symbols)
        {
            _symbols = symbols;
        }

        public string GetRandom(int amount)
        {
            string sequence = string.Empty;

            for (int i = 0; i < amount; i++)
                sequence += _symbols[Random.Range(0, _symbols.Length)];

            return sequence;
        }
    }
}
