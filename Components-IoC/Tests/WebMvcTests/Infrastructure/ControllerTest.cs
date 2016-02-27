using System.Web.Mvc;
using System.Web.Routing;

using Moq;
using ReusableLibrary.Abstractions.Models;
using ReusableLibrary.Abstractions.Services;
using ReusableLibrary.Web.Mvc.Integration;

using Public.WebMvcTests.Mocks;

using DependencyResolver = ReusableLibrary.Abstractions.IoC.DependencyResolver;
using IDependencyResolver = ReusableLibrary.Abstractions.IoC.IDependencyResolver;

namespace Public.WebMvcTests.Infrastructure
{
    public abstract class ControllerTest : Disposable
    {
        protected ControllerTest()
        {
            ValidationServiceMock = new Mock<IValidationService>(MockBehavior.Strict);
            DependencyResolverMock = new Mock<IDependencyResolver>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            HttpContextMock = new HttpContextMock();

            DependencyResolver.InitializeWith(DependencyResolverMock.Object);
            ModelBinders.Binders.DefaultBinder = new TrimStringModelBinder();
        }

        protected TController InitializeController<TController>(TController controller)
            where TController : ControllerBase
        {
            ControllerContext context = new ControllerContext(
                new RequestContext(HttpContextMock.Object, new RouteData()),
                controller);

            controller.ControllerContext = context;
            controller.ValueProvider = new SimpleValueProvider();
            return controller;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            DependencyResolver.Reset();
            ValidationServiceMock.VerifyAll();
            UnitOfWorkMock.VerifyAll();
            DependencyResolverMock.VerifyAll();
        }

        protected Mock<IValidationService> ValidationServiceMock { get; private set; }

        protected Mock<IDependencyResolver> DependencyResolverMock { get; private set; }

        protected Mock<IUnitOfWork> UnitOfWorkMock { get; private set; }

        protected HttpContextMock HttpContextMock { get; private set; }
    }
}