using System.Collections.Specialized;
using System.Web;

using Moq;

namespace Public.WebMvcTests.Mocks
{
    public class HttpRequestMock : Mock<HttpRequestBase>
    {
        public HttpRequestMock()
        {
            SetupGet(f => f.Form).Returns(new NameValueCollection());
            SetupGet(f => f.Headers).Returns(new NameValueCollection());
            SetupGet(r => r.QueryString).Returns(new NameValueCollection());
        }
    }
}