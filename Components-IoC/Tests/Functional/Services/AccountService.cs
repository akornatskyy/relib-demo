using System;
using System.Collections.Specialized;
using Public.FunctionalTests.Pages.Account;
using ReusableLibrary.WatiN;
using Tickets.Interface.Models;
using WatiN.Core;

namespace Public.FunctionalTests.Services
{
    public sealed class AccountService : AbstractService
    {
        public NameValueCollection Authenticate(UserCredentials userCredentials)
        {
            var page = Browser.Page<LogonPage>();
            return page.Container
                .Fill(userCredentials)
                .FillTextField(Find.ById("turingnumber"), "1234")
                .Submit()
                .OnErrorMessage(m => { throw new InvalidOperationException(m); })
                .ValidationErrors();
        }
    }
}
