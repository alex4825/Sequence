using Assets._Project.Develop.Runtime.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.CommonViews
{
    public class IconTextView : TextView, IView
    {
        [SerializeField] private Image _icon;

        public void SetIcon(Sprite icon) => _icon.sprite = icon;
    }
}
