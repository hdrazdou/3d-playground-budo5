using Zenject;

namespace Playground.Services.Bootstrap
{
    public class BootstrapServiceInstaller : Installer<BootstrapServiceInstaller>
    {
        #region Public methods

        public override void InstallBindings()
        {
            Container.Bind<BootstrapService>().AsSingle();
        }

        #endregion
    }
}