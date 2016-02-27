using System.IO;
using System.Web;

using Moq;

namespace Public.WebMvcTests.Mocks
{
    public class HttpResponseMock : Mock<HttpResponseBase>
    {
        public HttpResponseMock()
        {
            Output = new Mock<TextWriter>();
            OutputStream = new Mock<Stream>();
            Cache = new Mock<HttpCachePolicyBase>();

            SetupGet(m => m.Output).Returns(Output.Object);
            SetupGet(m => m.OutputStream).Returns(OutputStream.Object);
            SetupGet(m => m.Cache).Returns(Cache.Object);
        }

        public Mock<HttpCachePolicyBase> Cache { get; private set; }

        public Mock<TextWriter> Output { get; private set; }

        public Mock<Stream> OutputStream { get; private set; }
    }
}