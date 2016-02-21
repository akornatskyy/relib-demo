using System;
using System.Web;
using System.Web.Mvc;

using ReusableLibrary.Web.Integration;
using ReusableLibrary.Web.Mvc;

using Public.WebMvc.Constants;

namespace Public.WebMvc.Controllers
{
    [OutputCache(CacheProfile = CacheProfileNames.None)]
    public class HomeController : Controller
    {
        [HttpGet,
         OutputCache(VaryByCustom = VaryByCustomNames.Home,
            CacheProfile = CacheProfileNames.StaticPerUserContent)]
        public ActionResult Index()
        {
            ViewData["Message"] = Properties.Resources.Welcome;

            return View();
        }

        [HttpGet,
         OutputCache(VaryByCustom = VaryByCustomNames.Home,
            CacheProfile = CacheProfileNames.StaticPerUserContent)]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(CacheProfile = CacheProfileNames.StaticContent)]
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
            throw new NotImplementedException();
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