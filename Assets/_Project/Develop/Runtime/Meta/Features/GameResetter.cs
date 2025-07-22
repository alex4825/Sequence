using Assets._Project.Develop.Runtime.Infrastracture.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.SaveData;

namespace Assets._Project.Develop.Runtime.Meta.Features
{
    public class GameResetter : IDataWriter<PlayerData>
    {
        private WalletService _walletService;
        private VictoryDefeatCounter _victoryDefeatCounter;
        private int _resetCost;

        public GameResetter(WalletService walletService, VictoryDefeatCounter victoryDefeatCounter, PlayerDataProvider playerDataProvider, int resetCost)
        {
            _walletService = walletService;
            _victoryDefeatCounter = victoryDefeatCounter;
            _resetCost = resetCost;

            playerDataProvider.RegisterWriter(this);
        }

        public bool TryReset()
        {
            if (_walletService.Enough(CurrencyTypes.Gold, _resetCost))
            {
                _walletService.Spend(CurrencyTypes.Gold, _resetCost);
                _victoryDefeatCounter.Reset();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void WriteTo(PlayerData data)
        {
            data.VictoryCount = 0;
            data.DefeatCount = 0;
        }
    }
}
