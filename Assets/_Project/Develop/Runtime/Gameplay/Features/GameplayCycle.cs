using Assets._Project.Develop.Runtime.Configs.Meta;
using Assets._Project.Develop.Runtime.Gameplay.Utils;
using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.Infrastracture.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets.Develop.Runtime.Infrastracture.Gameplay.Views;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features
{
    public class GameplayCycle
    {
        private GameModes _gameModeType;
        private int _sequenceLenght;
        private GameplayView _gameplayView;
        private Popup _restartPopup;
        private DIContainer _container;

        private GameMode _gameMode;
        private SequenceGenerator _sequenceGenerator;
        private WalletService _walletService;

        private PlayerDataProvider _playerDataProvider;
        private int _winCost;
        private int _defeatCost;

        public GameplayCycle(GameModes gameModeType, int sequenceLenght, GameplayView gameplayView, Popup restartPopup, DIContainer container)
        {
            _gameModeType = gameModeType;
            _sequenceLenght = sequenceLenght;
            _gameplayView = gameplayView;
            _restartPopup = restartPopup;
            _container = container;

            _sequenceGenerator = new SequenceGenerator(_container.Resolve<SymbolsGetter>().GetFrom(_gameModeType));

            _playerDataProvider = _container.Resolve<PlayerDataProvider>();
            _walletService = _container.Resolve<WalletService>();

            CostsConfig costsConfig = _container.Resolve<ConfigsProviderService>().GetConfig<CostsConfig>();
            _winCost = costsConfig.WinCost;
            _defeatCost = costsConfig.DefeatCost;
        }

        public void Launch()
        {
            string randomSequence = _sequenceGenerator.GetRandom(_sequenceLenght);

            _gameMode = new(randomSequence);
            _gameplayView.SetText(randomSequence);

            _gameMode.Win += OnGameModeWin;
            _gameMode.Defeat += OnGameModeDefeat;

            _gameMode.Start();
        }

        public void Update()
        {
            _gameMode?.Update();
        }

        private void OnGameModeDefeat()
        {
            Debug.Log("Defeat");
            _walletService.Spend(CurrencyTypes.Gold, _defeatCost);
            _container.Resolve<ICoroutinesPerformer>().StartPerform(EndGame());
        }

        private void OnGameModeWin()
        {
            Debug.Log("Win");
            _walletService.Add(CurrencyTypes.Gold, _winCost);
            _container.Resolve<ICoroutinesPerformer>().StartPerform(EndGame());
        }

        private IEnumerator EndGame()
        {
            yield return _playerDataProvider.Save();
            Debug.Log($"Золота осталось: {_walletService.GetCurrency(CurrencyTypes.Gold).Value}");

            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;

            _restartPopup.SetText($"Press {KeyCode.F.ToString()} to restart or press {KeyCode.Escape.ToString()} to go to menu");

            _restartPopup.Show();

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape));

            if (Input.GetKeyDown(KeyCode.F))
                Launch();
            else if (Input.GetKeyDown(KeyCode.Escape))
                OpenMenu();

            _restartPopup.Hide();
        }

        private void OpenMenu()
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcesSwitchTo(Scenes.MainMenu));
        }
    }
}
