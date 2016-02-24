using ReusableLibrary.WatiN;
using WatiN.Core;
using Xunit;

using Public.FunctionalTests.Constants;
using Public.FunctionalTests.Infrastructure;
using Public.FunctionalTests.Pages.Account;
using Public.FunctionalTests.Pages.Home;
using Public.FunctionalTests.Parts;

namespace Public.FunctionalTests.Fixtures
{
    public sealed class HomeTest : IUseFixture<DefaultLifeTimeContainer>
    {
        private Browser browser;

        #region IUseFixture<DefaultLifeTimeContainer> Members

        public void SetFixture(DefaultLifeTimeContainer data)
        {
            browser = data.Application.Browser;
            var logoffLink = new HeaderQuickLinksMenuPart(browser).LogoffLink;
            if (logoffLink.Exists)
            {
                Assert.True(logoffLink.EnsureClick());
            }
        }

        #endregion

        [Fact]
        [Trait(TraitNames.Home, "UC1.1.1. Top menu")]
        public void TopMenu()
        {
            var menu = new HeaderMenuPart(browser);

            Assert.True(menu.AboutLink.EnsureClick());
            browser.Page<AboutPage>();

            Assert.True(menu.HomeLink.EnsureClick());
            browser.Page<HomePage>();

            Assert.True(menu.TicketsLink.EnsureClick());
            browser.Page<LogonPage>();
        }

        [Fact]
        [Trait(TraitNames.Home, "UC1.1.2. QuickLinks menu")]
        public void QuickLinks()
        {
            var menu = new HeaderQuickLinksMenuPart(browser);

            Assert.True(menu.LogonLink.EnsureClick());
            browser.Page<LogonPage>();
        }
    }
}