using Assets._Project.Develop.Runtime.Infrastracture.DI;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Features
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
