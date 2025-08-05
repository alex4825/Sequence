using Assets._Project.Develop.Runtime.UI.Core;
using TMPro;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Other
{
    public class NotifyPopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _message;

        public void SetTitle(string title) => _title.text = title;

        public void SetMessage(string message) => _message.text = message;
    }
}
