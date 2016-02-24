using ReusableLibrary.WatiN;
using WatiN.Core;

using Public.FunctionalTests.Constants;

namespace Public.FunctionalTests.Infrastructure
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
            : base(EnvironmentNames.PathToWebSite, EnvironmentNames.WebServerPort,
#if DEBUG
 false)
#else
 true)
#endif
        {
        }
    }
}