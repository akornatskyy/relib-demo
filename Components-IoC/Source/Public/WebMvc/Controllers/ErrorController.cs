using System.Net;
using System.Web.Mvc;

using ReusableLibrary.Web;
using ReusableLibrary.Web.Mvc;

using Public.WebMvc.Constants;

namespace Public.WebMvc.Controllers
{
    [OutputCache(CacheProfile = CacheProfileNames.None)]
    public sealed class ErrorController : Controller
    {
        [HttpGet]
        public ActionResult Http400()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var result = Request.CheckAspxErrorPath(RouteData.Values);
            return result ?? View();
        }

        [HttpGet]
        public ActionResult Http403()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            var result = Request.CheckAspxErrorPath(RouteData.Values);
            return result ?? View();
        }

        [HttpGet]
        public ActionResult Http404()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            var result = Request.CheckAspxErrorPath(RouteData.Values);
            return result ?? View();
        }

        [HttpGet]
        public ActionResult Http500()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = Request.CheckAspxErrorPath(RouteData.Values);
            return result ?? View();
        }

        [HttpGet,
        OutputCache(CacheProfile = CacheProfileNames.None)]
        public ActionResult HttpAntiForgery()
        {
            HttpContext.ResetCookies();
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var result = Request.CheckAspxErrorPath(RouteData.Values);
            return result ?? View();
        }

        [HttpGet]
        public ActionResult HttpRequestValidation()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var result = Request.CheckAspxErrorPath(RouteData.Values);
            return result ?? View();
        }

        [HttpGet]
        public ActionResult LimitExceeded()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var result = Request.CheckAspxErrorPath(RouteData.Values);
            return result ?? View();
        }

        [HttpGet]
        public ActionResult IpPolicy()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            var result = Request.CheckAspxErrorPath(RouteData.Values);
            return result ?? View();
        }
    }
}