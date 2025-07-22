using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;

namespace Assets.Develop.Runtime.Infrastracture.Gameplay.Mehanics
{
    public class SymbolsGetter
    {
        private DIContainer _container;

        public SymbolsGetter(DIContainer container)
        {
            _container = container;
        }

        public string GetFrom(GameModes gameMode)
             => _container.Resolve<ConfigsProviderService>().GetConfig<GameModesToSymbolsConfig>().GetSymbolsFrom(gameMode);
    }
}
