using System;
using System.Collections.Specialized;

using ReusableLibrary.WatiN;
using WatiN.Core;

using Public.FunctionalTests.Pages.Account;
using Tickets.Interface.Models;

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