using System;
using System.IO;

using ReusableLibrary.QualityAssurance.Profiling;
using Xunit;
using Xunit.Extensions;

using Tickets.Tests.Constants;
using Tickets.Tests.Fixtures;
using Tickets.Tests.Infrastructure;

namespace Tickets.Tests.Profiling
{
    public sealed class TicketRepositoryProfilingTest : AbstractProfilingTest<TicketRepositoryTest>
    {
        private readonly TextWriter logger = Console.Out;

        [Theory]
        [PropertyData("Threads")]
        [Trait(TraitNames.Profiling, "Ticket")]
        public void UpdateTicket(int threads)
        {
            var report = Profile(threads, test => test.UpdateTicket, DomainModelFactory.RandomUsers);

            logger.WriteLine(report);

            Assert.True(report.Succeed);
        }

        [Theory]
        [PropertyData("Threads")]
        [Trait(TraitNames.Profiling, "Ticket")]
        public void RetrieveMultiple(int threads)
        {
            var report = Profile(threads, test => test.RetrieveMultiple, DomainModelFactory.RandomTicketSpecifications);

            logger.WriteLine(report);

            Assert.True(report.Succeed);
        }

        [Theory]
        [PropertyData("Threads")]
        [Trait(TraitNames.Profiling, "Ticket")]
        public void ListTicketTypes(int threads)
        {
            var report = Profile(threads, test => test.ListTicketTypes, DomainModelFactory.RandomLanguageCodes);

            logger.WriteLine(report);

            Assert.True(report.Succeed);
        }
    }
}