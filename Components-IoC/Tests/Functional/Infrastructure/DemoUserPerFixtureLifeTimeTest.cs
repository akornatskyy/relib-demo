using Xunit;

using Public.FunctionalTests.Pages.Account;
using Public.FunctionalTests.Services;

namespace Public.FunctionalTests.Infrastructure
{
    public abstract class DemoUserPerFixtureLifeTimeTest<TService> : DefaultPerFixtureLifeTimeTest<TService>
        where TService : AbstractService, new()
    {
        public override void SetFixture(DefaultLifeTimeContainer data)
        {
            base.SetFixture(data);

            Browser.GoTo(data.Application.RelativeUri(LogonPage.Uri));
            var loginService = new AccountService
            {
                Browser = Browser
            };
            var errors = loginService.Authenticate(DomainModelFactory.CredentialsDemo());
            Assert.Empty(errors);
        }
    }
}