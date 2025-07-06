using TMPro;
using UnityEngine;

namespace Assets.Develop.Runtime.Infrastracture.Gameplay.Utils
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;

        public void SetText(string text) => _label.text = text;

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}
