using System.Configuration;
using System.Web.Mvc;

using ReusableLibrary.Abstractions.Bootstrapper;
using ReusableLibrary.Abstractions.Caching;
using ReusableLibrary.Captcha;
using ReusableLibrary.Web.Mvc.Integration;
using DependencyResolver = ReusableLibrary.Abstractions.IoC.DependencyResolver;

namespace Public.WebMvc.Helpers
{
    public class InitializeMvcSettings : IStartupTask
    {
        #region IStartupTask Members

        public void Execute()
        {
            DefaultModelBinder.ResourceClassKey = "SystemWebMvc";
            MvcHandler.DisableMvcResponseHeader = true;
            HtmlHelper.IdAttributeDotReplacement = "-";

            GlobalResource.ResourceClassKey = "ReusableLibraryWebMvc";
            Localization.DefaultLanguage = "en";
            Localization.Languages = new[] { "en", "ru" };

            CaptchaOptions.ResourceClassKey = "ReusableLibraryCaptcha";
            var options = ConfigurationManager.AppSettings["CaptchaOptions"] ?? string.Empty;            

            //// 1. Using default web cache            
            ////CaptchaBuilder.Current.Setup(new DefaultCaptchaFactory(
            ////    new CaptchaOptions(options)));

            //// 2. Using factory method to supply challenge code cache
            CaptchaBuilder.Current.Setup(new BitmapCaptchaFactory(
                new CaptchaOptions(options), () => DependencyResolver.Resolve<ICache>("Captcha")));

            options = ConfigurationManager.AppSettings["CaptchaOptions2"] ?? string.Empty;

            ////CaptchaBuilder.Current.Setup(new DefaultCaptchaFactory(
            ////    new CaptchaOptions(options)));

            CaptchaBuilder.Current.Setup(new BitmapCaptchaFactory(
                new CaptchaOptions(options), () => DependencyResolver.Resolve<ICache>("Captcha")));
        }

        #endregion
    }
}