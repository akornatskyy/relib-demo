using Public.FunctionalTests.Pages.Account;
using Public.FunctionalTests.Pages.Home;
using Public.FunctionalTests.Parts;
using ReusableLibrary.WatiN;
using WatiN.Core;
using Xunit;

namespace Public.FunctionalTests.Fixtures
{
    public sealed class HomeTest : IUseFixture<DefaultLifeTimeContainer>
    {
        private Browser m_browser;

        #region IUseFixture<DefaultLifeTimeContainer> Members

        public void SetFixture(DefaultLifeTimeContainer data)
        {
            m_browser = data.Application.Browser;
            var logoffLink = new HeaderQuickLinksMenuPart(m_browser).LogoffLink;
            if (logoffLink.Exists)
            {
                Assert.True(logoffLink.EnsureClick());
            }
        }

        #endregion

        [Fact]
        [Trait(Constants.TraitNames.Home, "UC1.1.1. Top menu")]
        public void TopMenu()
        {
            var menu = new HeaderMenuPart(m_browser);

            Assert.True(menu.AboutLink.EnsureClick());
            m_browser.Page<AboutPage>();

            Assert.True(menu.HomeLink.EnsureClick());
            m_browser.Page<HomePage>();

            Assert.True(menu.TicketsLink.EnsureClick());
            m_browser.Page<LogonPage>();
        }

        [Fact]
        [Trait(Constants.TraitNames.Home, "UC1.1.2. QuickLinks menu")]
        public void QuickLinks()
        {
            var menu = new HeaderQuickLinksMenuPart(m_browser);

            Assert.True(menu.LogonLink.EnsureClick());
            m_browser.Page<LogonPage>();
        }
    }
}
