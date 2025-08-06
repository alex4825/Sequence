using Assets._Project.Develop.Runtime.UI.Core;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Other
{
    public class SmallMessagePopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _message;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _disappearTime;

        public void SetMessage(string message) => _message.text = message;

        protected override void ModifyHideAnimation(Sequence animation)
        {
            animation
                .AppendInterval(_lifeTime)
                .Append(Body.DOFade(0, _disappearTime));
        }

        protected override void OnPostShow()
        {
            OnCloseEventCalled();
        }
    }
}
