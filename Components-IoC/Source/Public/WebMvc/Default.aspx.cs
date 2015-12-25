using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using ReusableLibrary.Web.Mvc.Integration;

namespace WebMvc
{
    public partial class Default : Page
    {
        public override void ProcessRequest(HttpContext context)
        {
            var contextWrapper = new HttpContextWrapper(HttpContext.Current);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Home");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("language", Localization.DefaultLanguage);
            var requestContext = new RequestContext(contextWrapper, routeData);
            var handler = new MvcHandler(requestContext) as IHttpHandler;
            handler.ProcessRequest(context);
        }
    }
}
