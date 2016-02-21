using System.Web;
using Public.WebMvc.Constants;
using ReusableLibrary.Abstractions.Bootstrapper;
using ReusableLibrary.Captcha;
using ReusableLibrary.Supplemental.System;
using ReusableLibrary.Unity;
using ReusableLibrary.Web;

namespace Public.WebMvc
{
    public class MvcApplication : HttpApplication
    {
        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            var key = CaptchaHandler.GetVaryByCustomString(context, custom);
            if (key != null)
            {
                return key;
            }

            var secure = Request.IsSchemeHttps() && custom.In(VaryByCustomNames.VaryByScheme)
                ? "s" : null;

            var user = custom.In(VaryByCustomNames.VaryByUser)
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