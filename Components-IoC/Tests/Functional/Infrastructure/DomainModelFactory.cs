using System;
using System.Text.RegularExpressions;
using Tickets.Interface.Models;
using ReusableLibrary.Supplemental.System;

namespace Public.FunctionalTests
{
    public static class DomainModelFactory
    {
        public static UserCredentials CredentialsDemo()
        {
            return new UserCredentials()
            {
                UserName = "demo",
                Password = "P@ssw0rd",
            };
        }

        public static TicketSpecification TicketSpecification()
        {
            return new TicketSpecification();
        }
    }
}
