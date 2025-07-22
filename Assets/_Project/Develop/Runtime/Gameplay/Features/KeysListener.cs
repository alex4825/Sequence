using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features
{
    public class KeysListener
    {
        public event Action<char> KeyPressed;

        public void Update()
        {
            if (!string.IsNullOrEmpty(Input.inputString))
                KeyPressed?.Invoke(Input.inputString[0]);
        }
    }
}
