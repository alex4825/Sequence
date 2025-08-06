using Assets._Project.Develop.Runtime.Infrastracture.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.SaveData;

namespace Assets._Project.Develop.Runtime.Meta.Features
{
    public class GameResetter : IDataWriter<PlayerData>
    {
        private readonly WalletService _walletService;
        private readonly WinDefeatCounter _winDefeatCounter;
        private readonly int _resetCost;

        private bool _isGameResetted;

        public GameResetter(WalletService walletService, WinDefeatCounter winDefeatCounter, PlayerDataProvider playerDataProvider, int resetCost)
        {
            _walletService = walletService;
            _winDefeatCounter = winDefeatCounter;
            _resetCost = resetCost;

            playerDataProvider.RegisterWriter(this);
        }

        public bool TryReset(out int notEnoughCount)
        {
            if (_walletService.Enough(CurrencyTypes.Gold, _resetCost))
            {
                _walletService.Spend(CurrencyTypes.Gold, _resetCost);
                _winDefeatCounter.Reset();
                _isGameResetted = true;
                notEnoughCount = 0;
                return true;
            }
            else
            {
                notEnoughCount = _resetCost - _walletService.GetCurrency(CurrencyTypes.Gold).Value;
                return false;
            }
        }

        public void WriteTo(PlayerData data)
        {
            if (_isGameResetted)
            {
                data.VictoryCount = 0;
                data.DefeatCount = 0;
                _isGameResetted = false;
            }
        }
    }
}
