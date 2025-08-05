using Assets._Project.Develop.Runtime.Configs.Meta;
using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.Infrastracture.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Infrastracture.Meta.Infrastracture
{
    public class MainMenuBootstrap : SceneBootsprap
    {
        private DIContainer _container;

        private WalletService _walletService;
        private VictoryDefeatCounter _victoryDefeatCounter;
        private GameResetter _gameResetter;

        private PlayerDataProvider _playerDataProvider;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }
        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация сцены главного меню.");

            _walletService = _container.Resolve<WalletService>();
            _victoryDefeatCounter = _container.Resolve<VictoryDefeatCounter>();
            _gameResetter = _container.Resolve<GameResetter>();
            _playerDataProvider = _container.Resolve<PlayerDataProvider>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт сцены главного меню.");

            //_container.Resolve<MetaToGameplayTransitor>().StartListen(_sequenceInputField, _gameModeSelector);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
                Debug.Log($"Золота осталось: {_walletService.GetCurrency(CurrencyTypes.Gold).Value}");

            if (Input.GetKeyDown(KeyCode.H))
                Debug.Log($"Побед: {_victoryDefeatCounter.VictoryCount.Value}, поражений: {_victoryDefeatCounter.DefeatCount.Value}");

            if (Input.GetKeyDown(KeyCode.R))
                if (_gameResetter.TryReset())
                {
                    _container.Resolve<ICoroutinesPerformer>().StartPerform(_playerDataProvider.Save());
                    Debug.Log($"Победы и поражения сброшены до 0.");
                }
                else
                {
                    Debug.Log($"Недостаточно золота для сброса. " +
                        $"Нужно {_container.Resolve<ConfigsProviderService>().GetConfig<CostsConfig>().GameResetCost}. " +
                        $"У вас {_walletService.GetCurrency(CurrencyTypes.Gold).Value}");
                }
        }
    }
}
