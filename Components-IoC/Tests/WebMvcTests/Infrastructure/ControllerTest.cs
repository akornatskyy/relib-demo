using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using IoC = ReusableLibrary.Abstractions.IoC;
using ReusableLibrary.Abstractions.Models;
using ReusableLibrary.Abstractions.Services;
using ReusableLibrary.Web.Mvc.Integration;

namespace Public.WebMvcTests
{
    public abstract class ControllerTest : Disposable
    {
        public ControllerTest()
        {
            ValidationServiceMock = new Mock<IValidationService>(MockBehavior.Strict);
            DependencyResolverMock = new Mock<IoC::IDependencyResolver>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
            HttpContextMock = new HttpContextMock();

            IoC::DependencyResolver.InitializeWith(DependencyResolverMock.Object);
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

            IoC::DependencyResolver.Reset();
            ValidationServiceMock.VerifyAll();
            UnitOfWorkMock.VerifyAll();
            DependencyResolverMock.VerifyAll();
        }

        protected Mock<IValidationService> ValidationServiceMock { get; private set; }

        protected Mock<IoC::IDependencyResolver> DependencyResolverMock { get; private set; }

        protected Mock<IUnitOfWork> UnitOfWorkMock { get; private set; }

        protected HttpContextMock HttpContextMock { get; private set; }
    }
}
