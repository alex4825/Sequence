using System;
using UnityEngine;

namespace Assets.Develop.Runtime.Infrastracture.Gameplay.Mehanics
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
