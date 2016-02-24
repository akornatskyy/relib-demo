using ReusableLibrary.WatiN;
using WatiN.Core;
using Xunit;

using Public.FunctionalTests.Parts;
using Public.FunctionalTests.Services;

namespace Public.FunctionalTests.Infrastructure
{
    public abstract class DefaultPerFixtureLifeTimeTest<TService> : IUseFixture<DefaultLifeTimeContainer>
        where TService : AbstractService, new()
    {
        public TService Service { get; private set; }

        public Browser Browser { get; private set; }

        #region IUseFixture<DefaultLifeTimeContainer> Members

        public virtual void SetFixture(DefaultLifeTimeContainer data)
        {
            var app = data.Application;
            Service = new TService();
            Browser = Service.Browser = app.Browser;
            var logoffLink = new HeaderQuickLinksMenuPart(Browser).LogoffLink;
            if (logoffLink.Exists)
            {
                Assert.True(logoffLink.EnsureClick());
            }
        }

        #endregion
    }
}