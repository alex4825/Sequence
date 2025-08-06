using Assets._Project.Develop.Runtime.Configs.Meta;
using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.Infrastracture.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Other;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.UI
{
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public WinDefeatPresenter CreateWinDefeatPresenter(TextListView view)
            => new WinDefeatPresenter(this, _container.Resolve<ViewsFactory>(), view, _container.Resolve<WinDefeatCounter>());

        public CountPresenter CreateCountPresenter(TextView view, IReadonlyVariable<int> count, string message)
            => new CountPresenter(count, view, message);

        public NotifyPopupPresenter CreateNotifyPopupPresenter(NotifyPopupView view)
        {
            return new NotifyPopupPresenter(view, _container.Resolve<ICoroutinesPerformer>());
        }

        public CurrencyPresenter CreateCurrencyPresenter(
            IconTextView view,
            IReadonlyVariable<int> currency,
            CurrencyTypes currencyType)
        {
            return new CurrencyPresenter(
                currency, 
                currencyType, 
                _container.Resolve<ConfigsProviderService>().GetConfig<CurrencyIconsConfig>(), 
                view);
        }

        public WalletPresenter CreateWalletPresenter(IconTextListView view)
        {
            return new WalletPresenter(_container.Resolve<WalletService>(), this, _container.Resolve<ViewsFactory>(), view);
        }
    }
}
