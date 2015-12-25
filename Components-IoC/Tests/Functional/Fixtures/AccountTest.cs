using System;
using Public.FunctionalTests.Pages.Account;
using Public.FunctionalTests.Services;
using ReusableLibrary.Supplemental.Collections;
using ReusableLibrary.Supplemental.System;
using Tickets.Interface.Models;
using Xunit;

namespace Public.FunctionalTests.Fixtures
{
    public sealed class AccountTest : DefaultPerFixtureLifeTimeTest<AccountService>
    {
        private readonly Random g_random = new Random();

        public override void SetFixture(DefaultLifeTimeContainer data)
        {
            base.SetFixture(data);
            Browser.GoTo(data.Application.RelativeUri(LogonPage.Uri));
        }

        [Fact]
        [Trait(Constants.TraitNames.Account, "UC2.1. Authenticate user")]
        public void Authenticate()
        {
            var errors = Service.Authenticate(DomainModelFactory.CredentialsDemo());
            Assert.Empty(errors);
        }

        [Fact]
        [Trait(Constants.TraitNames.Account, "UC2.1.1. Authentication validation")]
        public void Authenticate_Validation_Error()
        {
            var errors = Service.Authenticate(new UserCredentials());
            Assert.True(errors.HasKey("username"));
            Assert.True(errors.HasKey("password"));
        }

        [Fact]
        [Trait(Constants.TraitNames.Account, "UC2.1.2. Authentication unknown user")]
        public void Authenticate_UnknownUser()
        {
            Assert.Throws<InvalidOperationException>(() => Service.Authenticate(new UserCredentials() 
            { 
                UserName = g_random.NextWord(),
                Password = "12345678"
            }));
        }
    }
}
