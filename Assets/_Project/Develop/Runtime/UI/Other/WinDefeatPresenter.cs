using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.UI.Other
{
    public class WinDefeatPresenter : IPresenter
    {
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly ViewsFactory _viewsFactory;
        private readonly WinDefeatCounter _winDefeatCounter;

        private readonly TextListView _view;
        private readonly List<CountPresenter> _countPresenters = new();

        public WinDefeatPresenter(ProjectPresentersFactory presentersFactory, ViewsFactory viewsFactory, TextListView view, WinDefeatCounter winDefeatCounter)
        {
            _projectPresentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
            _winDefeatCounter = winDefeatCounter;
        }

        public void Initialize()
        {
            InitCountPresenter(_winDefeatCounter.WinCount, ViewIDs.WinView, "Wins: ");
            InitCountPresenter(_winDefeatCounter.DefeatCount, ViewIDs.DefeatView, "Defeats: ");
        }

        public void Dispose()
        {
            foreach (var countPresenter in _countPresenters)
            {
                _view.Remove(countPresenter.View);
                _viewsFactory.Release(countPresenter.View);
                countPresenter.Dispose();
            }

            _countPresenters.Clear();
        }

        private void InitCountPresenter(IReadonlyVariable<int> count, string viewID, string message)
        {
            TextView countView = _viewsFactory.Create<TextView>(viewID);
            _view.Add(countView);

            CountPresenter countPresenter = _projectPresentersFactory.CreateCountPresenter(countView, count, message);

            countPresenter.Initialize();
            _countPresenters.Add(countPresenter);
        }
    }
}
