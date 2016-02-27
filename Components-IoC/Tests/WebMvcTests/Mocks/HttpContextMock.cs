using System.Web;

using Moq;

namespace Public.WebMvcTests.Mocks
{
    public class HttpContextMock : Mock<HttpContextBase>
    {
        public HttpContextMock()
        {
            HttpApplicationState = new Mock<HttpApplicationStateBase>();
            HttpRequest = new HttpRequestMock();
            HttpResponse = new HttpResponseMock();
            HttpServerUtility = new Mock<HttpServerUtilityBase>();
            HttpSessionState = new Mock<HttpSessionStateBase>();

            SetupGet(c => c.Application).Returns(HttpApplicationState.Object);
            SetupGet(c => c.Request).Returns(HttpRequest.Object);
            SetupGet(c => c.Response).Returns(HttpResponse.Object);
            SetupGet(c => c.Server).Returns(HttpServerUtility.Object);
            SetupGet(c => c.Session).Returns(HttpSessionState.Object);
        }

        public Mock<HttpApplicationStateBase> HttpApplicationState { get; private set; }

        public HttpRequestMock HttpRequest { get; private set; }

        public HttpResponseMock HttpResponse { get; private set; }

        public Mock<HttpServerUtilityBase> HttpServerUtility { get; private set; }

        public Mock<HttpSessionStateBase> HttpSessionState { get; private set; }
    }
}