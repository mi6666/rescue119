using VContainer.Unity;

namespace Installer
{
    public class InstallerBase: LifetimeScope
    {
        private void Reset()
        {
            autoRun = false;
        }
    }
}