using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Project.Develop.Runtime.UI.Other
{
    public class NotifyPopupPresenter : PopupPresenterBase
    {
        NotifyPopupView _view;

        public NotifyPopupPresenter(NotifyPopupView view, ICoroutinesPerformer coroutinesPerformer) : base(coroutinesPerformer)
        {
            _view = view;
        }

        protected override PopupViewBase PopupView => _view;

        public void SetView(string title, string message)
        {
            _view.SetTitle(title);
            _view.SetMessage(message);
        }
    }
}
