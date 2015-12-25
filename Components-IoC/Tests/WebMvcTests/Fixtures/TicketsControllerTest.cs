using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using Public.WebMvc.Controllers;
using Public.WebMvc.Models;
using Tickets.Interface.Models;
using Tickets.Interface.Services;
using Xunit;
using ReusableLibrary.Abstractions.Models;
using ReusableLibrary.Abstractions.Services;

namespace Public.WebMvcTests.Fixtures
{
    public sealed class TicketsControllerTest : ControllerTest
    {
        private readonly Mock<IMementoService> m_mementoServiceMock;
        private readonly Mock<ITicketService> m_ticketServiceMock;
        private readonly TicketsController m_controller;

        public TicketsControllerTest()
        {
            m_mementoServiceMock = new Mock<IMementoService>(MockBehavior.Strict);
            m_ticketServiceMock = new Mock<ITicketService>(MockBehavior.Strict);
            m_controller = InitializeController(new TicketsController(
                ValidationServiceMock.Object, 
                m_mementoServiceMock.Object,
                m_ticketServiceMock.Object));
        }

        [Fact]
        [Trait(Constants.TraitNames.Controller, "TicketsController")]
        public void Index()
        {
            // Arrange
            SetupViewDataLists();
            m_mementoServiceMock
                .Setup(memento => memento.Load<TicketSpecification>())
                .Returns(new TicketSpecification());

            // Act
            var result = m_controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result.ViewName);

            var viewdata = result.ViewData.Model as TicketSearchViewData;
            Assert.NotNull(viewdata);
            Assert.NotNull(viewdata.Specification);
            Assert.NotNull(viewdata.TicketTypeSelectList);

            Assert.Equal(TicketType.Any.Key, viewdata.Specification.TicketTypeId);
        }

        [Fact]
        [Trait(Constants.TraitNames.Controller, "TicketsController")]
        public void Search_Validation_Failed()
        {
            // Arrange
            SetupViewDataLists();
            ValidationServiceMock
                .Setup(validationService => validationService.Validate(It.IsAny<TicketSpecification>()))
                .Returns(false);

            // Act
            var result = m_controller.Search() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result.ViewName);
        }

        [Fact]
        [Trait(Constants.TraitNames.Controller, "TicketsController")]
        public void Search()
        {
            // Arrange
            var spec = DomainModelFactory.TicketSpecification();
            var items = DomainModelFactory.PagedList_TicketSearchResult(2);

            m_controller.ValueProvider = DomainModelFactory.ValueProvider(spec);
            ValidationServiceMock
                .Setup(validationService => validationService.Validate(It.IsAny<TicketSpecification>()))
                .Callback<TicketSpecification>(s => AssertHelper.Equal(spec, s))
                .Returns(true);
            m_mementoServiceMock
                .Setup(memento => memento.Save(It.IsAny<TicketSpecification>()))
                .Returns(true);
            m_ticketServiceMock
                .Setup(ticketService => ticketService.SearchTickets(It.IsAny<TicketSpecification>(), null, null))
                .Returns(items);

            // Act
            var result = m_controller.Search() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("List", result.ViewName);

            var viewdata = result.ViewData.Model as TicketListViewData;
            Assert.NotNull(viewdata);
            Assert.NotNull(viewdata.Specification);
            AssertHelper.Equal(spec, viewdata.Specification);
            Assert.NotNull(viewdata.Items);            
            Assert.Equal(items, viewdata.Items);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                m_mementoServiceMock.VerifyAll();
                m_ticketServiceMock.VerifyAll();                
            }
        }

        private void SetupViewDataLists()
        {
            m_ticketServiceMock.Setup(service => service.DefaultTicketType)
                .Returns(TicketType.Any);
            m_ticketServiceMock.Setup(service => service.TicketTypes)
                .Returns(new List<TicketType>());
        }

        [STAThread]
        internal static void Main()
        {
            try
            {
                using (var test = new TicketsControllerTest())
                {
                    try
                    {
                        test.Search();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(ex);
            }

            Environment.Exit(0);
        }
    }
}
