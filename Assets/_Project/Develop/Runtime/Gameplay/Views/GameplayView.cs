using TMPro;
using UnityEngine;

namespace Assets.Develop.Runtime.Infrastracture.Gameplay.Views
{
    public class GameplayView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;

        public void SetText(string text)
        {
            _label.text = text;
        }
    }
}
