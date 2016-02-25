using System.Collections.Generic;
using System.Linq;

using ReusableLibrary.Abstractions.Repository;
using ReusableLibrary.QualityAssurance.Repository;
using ReusableLibrary.Supplemental.Collections;
using Xunit;
using Xunit.Extensions;

using Tickets.Interface.Models;
using Tickets.Module.Repository;
using Tickets.Tests.Constants;
using Tickets.Tests.Infrastructure;

namespace Tickets.Tests.Fixtures
{
    public sealed class TicketRepositoryTest : AbstractRepositoryTest<TicketRepository, TicketsDataContext>
    {
        public static IEnumerable<object[]> Users
        {
            get
            {
                return DomainModelFactory.RandomUsers().Take(10).ToPropertyData();
            }
        }

        public static IEnumerable<object[]> TicketSpecifications
        {
            get
            {
                return DomainModelFactory.RandomTicketSpecifications().Take(10).ToPropertyData();
            }
        }

        [Theory]
        [PropertyData("Users")]
        [Trait(TraitNames.Repository, "Ticket")]
        public void UpdateTicket(string username)
        {
            // Arrange

            // Act
            // Repository.UpdateTicket(username, ...);
            // SubmitChanges();

            // Assert    
        }

        [Theory]
        [PropertyData("TicketSpecifications")]
        [Trait(TraitNames.Repository, "Ticket")]
        public void RetrieveMultiple(TicketSpecification spec)
        {
            // Arrange
            var req = new RetrieveMultipleRequest<TicketSpecification>(spec)
            {
                PageSize = 50
            };

            // Act
            var result = Repository.RetrieveMultiple(req);

            // Assert 
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("en")]
        [Trait(TraitNames.Repository, "Ticket")]
        public void ListTicketTypes(string languageCode)
        {
            // Arrange

            // Act
            var result = Repository.ListTicketTypes(languageCode).ToList();

            // Assert 
            Assert.Equal(5, result.Count);
        }
    }
}