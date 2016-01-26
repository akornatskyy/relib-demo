using System.Web;
using ReusableLibrary.Abstractions.Bootstrapper;
using ReusableLibrary.Captcha;
using ReusableLibrary.Supplemental.System;
using ReusableLibrary.Unity;
using ReusableLibrary.Web;

namespace Public.WebMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            var key = CaptchaHandler.GetVaryByCustomString(context, custom);
            if (key != null)
            {
                return key;
            }

            var secure = Request.IsSchemeHttps() && custom.In(Constants.VaryByCustomNames.VaryByScheme)
                ? "s" : null;

            var user = custom.In(Constants.VaryByCustomNames.VaryByUser)
                ? string.Concat(custom, context.User.Identity.Name) : null;

            return string.Concat(secure, user);
        }

        protected void Application_Start()
        {
            UnityBootstrapLoader.Initialize(UnityBootstrapLoader.LoadConfigFilesFromAppSettings());
            BootstrapLoader.Start();
        }

        protected void Application_End()
        {
            BootstrapLoader.End();
        }
    }
}