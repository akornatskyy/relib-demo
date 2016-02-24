using Tickets.Interface.Models;

namespace Public.FunctionalTests.Infrastructure
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