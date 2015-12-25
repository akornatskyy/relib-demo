using System;
using System.Web;
using System.Web.Mvc;
using ReusableLibrary.Web.Integration;
using ReusableLibrary.Web.Mvc;

namespace Public.WebMvc.Controllers
{
    [OutputCache(CacheProfile = Constants.CacheProfileNames.None)]
    public class HomeController : Controller
    {
        [HttpGet,
         OutputCache(VaryByCustom = Constants.VaryByCustomNames.Home,
            CacheProfile = Constants.CacheProfileNames.StaticPerUserContent)]
        public ActionResult Index()
        {
            ViewData["Message"] = Properties.Resources.Welcome;

            return View();
        }

        [HttpGet,
         OutputCache(VaryByCustom = Constants.VaryByCustomNames.Home,
            CacheProfile = Constants.CacheProfileNames.StaticPerUserContent)]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(CacheProfile = Constants.CacheProfileNames.StaticContent)]
        public ActionResult CacheSubstitution()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BadRequest()
        {
            return new BadRequestResult();
        }

        [HttpGet]
        public ActionResult Forbidden()
        {
            return new ForbiddenResult();
        }

        [HttpGet]
        public ActionResult FileNotFound()
        {
            return new FileNotFoundResult();
        }

        [HttpGet]
        public ActionResult InternalError()
        {
            throw new ArgumentNullException("test");
        }

        [HttpGet]
        public ActionResult AntiForgery()
        {
            throw new HttpAntiForgeryException();
        }

        [HttpGet]
        public ActionResult RequestValidation()
        {
            throw new HttpRequestValidationException();
        }

        [HttpGet]
        public ActionResult IpPolicy()
        {
            throw new IpPolicyException();
        }
    }
}
