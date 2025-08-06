using Assets._Project.Develop.Runtime.UI.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public abstract class PopupService : IDisposable
    {
        protected readonly ViewsFactory ViewsFactory;

        private readonly ProjectPresentersFactory _presentersFactory;

        private readonly Dictionary<PopupPresenterBase, PopupInfo> _presenterToInfo = new();

        protected PopupService(ViewsFactory viewsFactory, ProjectPresentersFactory presentersFactory)
        {
            ViewsFactory = viewsFactory;
            _presentersFactory = presentersFactory;
        }

        protected abstract Transform PopupLayer { get; }

        public SmallMessagePopupPresenter OpenSmallMessagePopup()
        {
            SmallMessagePopupView view = ViewsFactory.Create<SmallMessagePopupView>(ViewIDs.SmallMessagePopupView, PopupLayer);

            SmallMessagePopupPresenter popup = _presentersFactory.CreateSmallMessagePopupPresenter(view);

            OnPopupCreated(popup, view);

            return popup;
        }

        public NotifyPopupPresenter OpenNotifyPopup()
        {
            NotifyPopupView view = ViewsFactory.Create<NotifyPopupView>(ViewIDs.NotifyPopupView, PopupLayer);

            NotifyPopupPresenter popup = _presentersFactory.CreateNotifyPopupPresenter(view);

            OnPopupCreated(popup, view);

            return popup;
        }

        public void ClosePopup(PopupPresenterBase popup)
        {
            popup.CloseRequest -= ClosePopup;

            popup.Hide(() =>
            {
                if (_presenterToInfo.Keys.Contains(popup))
                    _presenterToInfo[popup].ClosedCallback?.Invoke();

                DisposeFor(popup);
                _presenterToInfo.Remove(popup);
            });
        }

        public void Dispose()
        {
            foreach (PopupPresenterBase popup in _presenterToInfo.Keys)
            {
                popup.CloseRequest -= ClosePopup;
                DisposeFor(popup);
            }

            _presenterToInfo.Clear();
        }

        protected void OnPopupCreated(PopupPresenterBase popup, PopupViewBase view, Action closedCallback = null)
        {
            PopupInfo popupInfo = new PopupInfo(view, closedCallback);

            _presenterToInfo.Add(popup, popupInfo);
            popup.Initialize();
            popup.Show();

            popup.CloseRequest += ClosePopup;
        }

        private void DisposeFor(PopupPresenterBase popup)
        {
            popup.Dispose();

            if (_presenterToInfo.Keys.Contains(popup))
                ViewsFactory.Release(_presenterToInfo[popup].View);
        }

        private class PopupInfo
        {
            public PopupInfo(PopupViewBase viewBase, Action closedCallback)
            {
                View = viewBase;
                ClosedCallback = closedCallback;
            }

            public PopupViewBase View { get; }
            public Action ClosedCallback { get; }


        }
    }
}
