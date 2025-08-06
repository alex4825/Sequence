using Assets._Project.Develop.Runtime.Configs.Meta;
using Assets._Project.Develop.Runtime.Gameplay.Features;
using Assets._Project.Develop.Runtime.Infrastracture.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Other;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayPresenter : IPresenter
    {
        private readonly GameplayScreenView _view;

        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        private readonly PlayerDataProvider _playerDataProvider;
        private readonly CostsConfig _costsConfig;

        private readonly GameplayCycle _gameplayCycle;
        private readonly WalletService _walletService;
        private readonly WinDefeatCounter _winDefeatCounter;

        private readonly GameplayPopupService _gameplayPopupService;

        IDisposable _disposableInputText;
        IDisposable _disposableSequence;

        public GameplayPresenter(
            GameplayScreenView view,
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer,
            PlayerDataProvider playerDataProvider,
            CostsConfig costsConfig,
            GameplayCycle gameplayCycle,
            WalletService walletService,
            WinDefeatCounter winDefeatCounter,
            GameplayPopupService gameplayPopupService)
        {
            _view = view;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _playerDataProvider = playerDataProvider;
            _costsConfig = costsConfig;
            _gameplayCycle = gameplayCycle;
            _walletService = walletService;
            _winDefeatCounter = winDefeatCounter;
            _gameplayPopupService = gameplayPopupService;
        }

        public void Initialize()
        {
            _view.GoToMenuButtonClicked += OnGoToMenuButtonClicked;
            _gameplayCycle.GameWin += OnGameWin;
            _gameplayCycle.GameDefeat += OnGameDefeat;
            _disposableInputText = _gameplayCycle.InputText.Subscribe(OnInputTextChanged);
            _disposableSequence = _gameplayCycle.Sequence.Subscribe(OnSequenceChanged);

            _gameplayCycle.Launch();
        }

        private void OnSequenceChanged(string oldValue, string newValue)
        {
            _view.SetSequenceText(newValue);
        }

        private void OnInputTextChanged(string oldValue, string newValue)
        {
            _view.SetInputText(newValue);
        }

        public void Dispose()
        {
            _view.GoToMenuButtonClicked -= OnGoToMenuButtonClicked;
            _gameplayCycle.GameWin -= OnGameWin;
            _gameplayCycle.GameDefeat -= OnGameDefeat;

            _disposableInputText.Dispose();
            _disposableSequence.Dispose();
        }

        private void OnGameWin()
        {
            _walletService.Add(CurrencyTypes.Gold, _costsConfig.WinCost);
            _winDefeatCounter.AddVictory();

            SmallMessagePopupPresenter smallMessagePopup = _gameplayPopupService.OpenSmallMessagePopup();
            smallMessagePopup.SetView("You win!");

            RestartGame();
        }

        private void OnGameDefeat()
        {
            if (_walletService.Enough(CurrencyTypes.Gold, _costsConfig.DefeatCost))
                _walletService.Spend(CurrencyTypes.Gold, _costsConfig.DefeatCost);
            else
                _walletService.Spend(CurrencyTypes.Gold, _walletService.GetCurrency(CurrencyTypes.Gold).Value);

            _winDefeatCounter.AddDefeat();

            SmallMessagePopupPresenter smallMessagePopup = _gameplayPopupService.OpenSmallMessagePopup();
            smallMessagePopup.SetView("You lose...");

            RestartGame();
        }

        private void RestartGame()
        {
            _coroutinesPerformer.StartPerform(_playerDataProvider.Save());

            _gameplayCycle.Launch();
        }

        private void OnGoToMenuButtonClicked()
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcesSwitchTo(Scenes.MainMenu));
        }
    }
}
