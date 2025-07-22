using Assets._Project.Develop.Runtime.Infrastracture.Meta.Features.Wallet;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement.SaveData
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;

        public int WinCount;

        public int DefeatCount;
    }
}
