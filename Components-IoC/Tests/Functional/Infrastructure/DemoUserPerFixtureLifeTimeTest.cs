using Public.FunctionalTests.Pages.Account;
using Public.FunctionalTests.Services;
using Xunit;

namespace Public.FunctionalTests
{
    public abstract class DemoUserPerFixtureLifeTimeTest<TService> : DefaultPerFixtureLifeTimeTest<TService>
        where TService : AbstractService, new()
    {
        public override void SetFixture(DefaultLifeTimeContainer data)
        {
            base.SetFixture(data);

            Browser.GoTo(data.Application.RelativeUri(LogonPage.Uri));
            var loginService = new AccountService();
            loginService.Browser = Browser;
            var errors = loginService.Authenticate(DomainModelFactory.CredentialsDemo());
            Assert.Empty(errors);
        }
    }
}
