using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Moq;
using ReusableLibrary.Abstractions.Services;
using Xunit;

using Public.WebMvc.Controllers;
using Public.WebMvc.Models;
using Public.WebMvcTests.Constants;
using Public.WebMvcTests.Helpers;
using Public.WebMvcTests.Infrastructure;
using Tickets.Interface.Models;
using Tickets.Interface.Services;

namespace Public.WebMvcTests.Fixtures
{
    public sealed class TicketsControllerTest : ControllerTest
    {
        private readonly Mock<IMementoService> mockMementoService;
        private readonly Mock<ITicketService> mockTicketService;
        private readonly TicketsController controller;

        public TicketsControllerTest()
        {
            this.mockMementoService = new Mock<IMementoService>(MockBehavior.Strict);
            this.mockTicketService = new Mock<ITicketService>(MockBehavior.Strict);
            this.controller = InitializeController(new TicketsController(
                ValidationServiceMock.Object, 
                mockMementoService.Object,
                mockTicketService.Object));
        }

        [Fact]
        [Trait(TraitNames.Controller, "TicketsController")]
        public void Index()
        {
            // Arrange
            SetupViewDataLists();
            mockMementoService
                .Setup(memento => memento.Load<TicketSpecification>())
                .Returns(new TicketSpecification());

            // Act
            var result = controller.Index() as ViewResult;

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
        [Trait(TraitNames.Controller, "TicketsController")]
        public void Search_Validation_Failed()
        {
            // Arrange
            SetupViewDataLists();
            ValidationServiceMock
                .Setup(validationService => validationService.Validate(It.IsAny<TicketSpecification>()))
                .Returns(false);

            // Act
            var result = controller.Search() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(string.Empty, result.ViewName);
        }

        [Fact]
        [Trait(TraitNames.Controller, "TicketsController")]
        public void Search()
        {
            // Arrange
            var spec = DomainModelFactory.TicketSpecification();
            var items = DomainModelFactory.PagedList_TicketSearchResult(2);

            controller.ValueProvider = DomainModelFactory.ValueProvider(spec);
            ValidationServiceMock
                .Setup(validationService => validationService.Validate(It.IsAny<TicketSpecification>()))
                .Callback<TicketSpecification>(s => AssertHelper.Equal(spec, s))
                .Returns(true);
            mockMementoService
                .Setup(memento => memento.Save(It.IsAny<TicketSpecification>()))
                .Returns(true);
            mockTicketService
                .Setup(ticketService => ticketService.SearchTickets(It.IsAny<TicketSpecification>(), null, null))
                .Returns(items);

            // Act
            var result = controller.Search() as ViewResult;

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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                mockMementoService.VerifyAll();
                mockTicketService.VerifyAll();
            }
        }

        private void SetupViewDataLists()
        {
            mockTicketService.Setup(service => service.DefaultTicketType)
                .Returns(TicketType.Any);
            mockTicketService.Setup(service => service.TicketTypes)
                .Returns(new List<TicketType>());
        }
    }
}