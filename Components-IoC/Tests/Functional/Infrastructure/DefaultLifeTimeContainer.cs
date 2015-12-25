using ReusableLibrary.WatiN;
using WatiN.Core;

namespace Public.FunctionalTests
{
    public class DefaultLifeTimeContainer : ApplicationLifeTimeContainer<IE>
    {
        static DefaultLifeTimeContainer()
        {
#if DEBUG
            Settings.MakeNewIeInstanceVisible = true;
#else
            Settings.MakeNewIeInstanceVisible = false;
#endif
        }

        public DefaultLifeTimeContainer()
            : base(Constants.EnvironmentNames.PathToWebSite, Constants.EnvironmentNames.WebServerPort,
#if DEBUG
 false)
#else
 true)
#endif
        {
        }
    }
}
