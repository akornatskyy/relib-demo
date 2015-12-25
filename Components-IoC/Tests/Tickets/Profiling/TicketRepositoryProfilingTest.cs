using System.Collections.Generic;
using System.IO;
using ReusableLibrary.QualityAssurance.Profiling;
using ReusableLibrary.Supplemental.Collections;
using Tickets.Interface.Models;
using Tickets.Tests.Fixtures;
using Tickets.Tests.Infrastructure;
using Xunit;
using Xunit.Extensions;

namespace Tickets.Tests.Profiling
{
    public sealed class TicketRepositoryProfilingTest : AbstractProfilingTest<TicketRepositoryTest>
    {
        private static IEnumerable<object[]> g_threads = (new[] { 1, 2, 4 }).ToPropertyData();
        
        private readonly TextWriter m_logger = System.Console.Out;

        public static IEnumerable<object[]> Threads
        {
            get
            {
                return g_threads;
            }
        }

        [Theory]
        [PropertyData("Threads")]
        [Trait(Constants.TraitNames.Profiling, "Ticket")]
        public void UpdateTicket(int threads)
        {
            var report = Profile<string>(threads, test => test.UpdateTicket, DomainModelFactory.RandomUsers);

            m_logger.WriteLine(report);

            Assert.True(report.Succeed);
        }

        [Theory]
        [PropertyData("Threads")]
        [Trait(Constants.TraitNames.Profiling, "Ticket")]
        public void RetrieveMultiple(int threads)
        {
            var report = Profile<TicketSpecification>(threads, test => test.RetrieveMultiple, DomainModelFactory.RandomTicketSpecifications);

            m_logger.WriteLine(report);

            Assert.True(report.Succeed);
        }

        [Theory]
        [PropertyData("Threads")]
        [Trait(Constants.TraitNames.Profiling, "Ticket")]
        public void ListTicketTypes(int threads)
        {
            var report = Profile<string>(threads, test => test.ListTicketTypes, DomainModelFactory.RandomLanguageCodes);

            m_logger.WriteLine(report);

            Assert.True(report.Succeed);
        }
    }
}
