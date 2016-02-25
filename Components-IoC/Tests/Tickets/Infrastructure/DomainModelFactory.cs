using System;
using System.Collections.Generic;
using System.Linq;

using ReusableLibrary.Supplemental.System;

using Tickets.Interface.Models;
using Tickets.Tests.Properties;

namespace Tickets.Tests.Infrastructure
{
    public static class DomainModelFactory
    {
        private static readonly Random Rnd = new Random();
        private static readonly IEnumerable<string> Users;

        static DomainModelFactory()
        {
            Users = Resources.ListUsers
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList().AsReadOnly();
        }

        public static IEnumerable<string> RandomUsers()
        {
            return Rnd.Shuffle(Users);
        }

        public static IEnumerable<TicketSpecification> RandomTicketSpecifications()
        {
            return Rnd.NextSequence(500, 600, i => new TicketSpecification()
            {
                IncludeClosed = Rnd.NextBoolean(),
                TicketTypeId = Rnd.Next(1, 3, 5, 10),
                Title = Rnd.Next("computer", "mail", "install")
            });
        }

        public static IEnumerable<string> RandomLanguageCodes()
        {
            return new[] { "en", "ru" };
        }
    }
}