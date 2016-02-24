using System;

using ReusableLibrary.Supplemental.Collections;
using ReusableLibrary.Supplemental.System;
using Xunit;

using Public.FunctionalTests.Constants;
using Public.FunctionalTests.Infrastructure;
using Public.FunctionalTests.Pages.Account;
using Public.FunctionalTests.Services;
using Tickets.Interface.Models;

namespace Public.FunctionalTests.Fixtures
{
    public sealed class AccountTest : DefaultPerFixtureLifeTimeTest<AccountService>
    {
        private readonly Random random = new Random();

        public override void SetFixture(DefaultLifeTimeContainer data)
        {
            base.SetFixture(data);
            Browser.GoTo(data.Application.RelativeUri(LogonPage.Uri));
        }

        [Fact]
        [Trait(TraitNames.Account, "UC2.1. Authenticate user")]
        public void Authenticate()
        {
            var errors = Service.Authenticate(DomainModelFactory.CredentialsDemo());
            Assert.Empty(errors);
        }

        [Fact]
        [Trait(TraitNames.Account, "UC2.1.1. Authentication validation")]
        public void Authenticate_Validation_Error()
        {
            var errors = Service.Authenticate(new UserCredentials());
            Assert.True(errors.HasKey("username"));
            Assert.True(errors.HasKey("password"));
        }

        [Fact]
        [Trait(TraitNames.Account, "UC2.1.2. Authentication unknown user")]
        public void Authenticate_UnknownUser()
        {
            Assert.Throws<InvalidOperationException>(() => Service.Authenticate(new UserCredentials() 
            { 
                UserName = random.NextWord(),
                Password = "12345678"
            }));
        }
    }
}