using Assets._Project.Develop.Runtime.Gameplay.Infrastracture;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Other;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _view;
        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly MainMenuPopupService _mainMenuPopupService;

        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        private readonly List<IPresenter> _childPresenters = new();

        public MainMenuScreenPresenter(
            MainMenuScreenView view,
            ProjectPresentersFactory projectPresentersFactory,
            MainMenuPopupService popupService,
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer)
        {
            _view = view;
            _projectPresentersFactory = projectPresentersFactory;
            _mainMenuPopupService = popupService;
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
        }

        public void Initialize()
        {
            _view.StartGameplayButtonClicked += OnStartGameplayButtonClicked;

            CreateWalletPresenter();
            CreateWinDefeatPresenter();

            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Initialize();
        }

        public void Dispose()
        {
            _view.StartGameplayButtonClicked -= OnStartGameplayButtonClicked;

            foreach (IPresenter childPresenter in _childPresenters)
                childPresenter.Dispose();

            _childPresenters.Clear();
        }

        private void CreateWinDefeatPresenter()
        {
            WinDefeatPresenter winDefeatPresenter = _projectPresentersFactory.CreateWinDefeatPresenter(_view.WinDefeatView);

            _childPresenters.Add(winDefeatPresenter);
        }

        private void CreateWalletPresenter()
        {
            WalletPresenter walletPresenter = _projectPresentersFactory.CreateWalletPresenter(_view.WalletView);

            _childPresenters.Add(walletPresenter);
        }

        private void OnStartGameplayButtonClicked(GameModes gameMode, string lengthText)
        {
            if (int.TryParse(lengthText, out int sequenceLength) && sequenceLength > 0)
            {
                StartGameplay(gameMode, sequenceLength);
            }
            else
            {
                NotifyPopupPresenter notifyPopupPresenter = _mainMenuPopupService.OpenNotifyPopup();
                notifyPopupPresenter.SetView($"Warning!", $"{lengthText} isn't correct value. Enter positive int number greater than 0.");

                Debug.Log("Enter positive int number greater than 0");
            }
        }

        private void StartGameplay(GameModes gameMode, int sequenceLength)
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcesSwitchTo(Scenes.Gameplay, new GameplayInputArgs(gameMode, sequenceLength)));
        }
    }
}
