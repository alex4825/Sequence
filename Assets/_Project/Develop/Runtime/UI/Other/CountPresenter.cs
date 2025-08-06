using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.UI.Other
{
    public class CountPresenter : IPresenter
    {
        private readonly IReadonlyVariable<int> _count;
        private TextView _textCountView;

        private IDisposable _disposable;
        private readonly string _firstMessage;

        public CountPresenter(IReadonlyVariable<int> count, TextView textCountView, string firstMessage)
        {
            _count = count;
            _textCountView = textCountView;
            _firstMessage = firstMessage;
        }

        public TextView View => _textCountView;

        public void Initialize()
        {
            UpdateView(_count.Value);

            _disposable = _count.Subscribe(OnCountChanged);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnCountChanged(int oldValue, int newValue) => UpdateView(newValue);

        private void UpdateView(int newValue) => _textCountView.SetText(_firstMessage + newValue.ToString());
    }
}
