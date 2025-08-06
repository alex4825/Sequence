using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;

namespace Assets._Project.Develop.Runtime.UI.Other
{
    public class SmallMessagePopupPresenter : PopupPresenterBase
    {
        SmallMessagePopupView _view;

        public SmallMessagePopupPresenter(SmallMessagePopupView view, ICoroutinesPerformer coroutinesPerformer) : base(coroutinesPerformer)
        {
            _view = view;
        }

        protected override PopupViewBase PopupView => _view;

        public void SetView(string message)
        {
            _view.SetMessage(message);
        }
    }
}
