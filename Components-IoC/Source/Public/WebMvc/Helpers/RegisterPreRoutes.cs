using System.Web.Mvc;
using System.Web.Routing;
using ReusableLibrary.Abstractions.Bootstrapper;
using ReusableLibrary.Web.Mvc.Integration;
using ReusableLibrary.Web.Routing;

namespace Public.WebMvc.Helpers
{
    public sealed class RegisterPreRoutes : IStartupTask
    {
        #region IStartupTask Members

        public void Execute()
        {
            var routes = RouteTable.Routes;
            routes.MapRoute("SchemeExample", "{language}/welcome-scheme",
                new
                {
                    controller = "Home",
                    action = "About",
                    id = string.Empty,
                    language = Localization.DefaultLanguage,
                    scheme = "https"
                },
                new
                {
                    scheme = new SchemeRouteConstraint(),
                    language = new ChoiceRouteConstraint(Localization.Languages)
                });
            routes.MapRoute("DomainExample", "{language}/welcome-domain",
                new
                {
                    controller = "Home",
                    action = "About",
                    id = string.Empty,
                    language = Localization.DefaultLanguage,
                    domain = "example.org"
                },
                new
                {
                    domain = new DomainRouteConstraint(),
                    language = new ChoiceRouteConstraint(Localization.Languages)
                });
            routes.MapRoute("SchemeAndDomainExample", "{language}/welcome",
                new
                {
                    controller = "Home",
                    action = "About",
                    id = string.Empty,
                    language = Localization.DefaultLanguage,
                    scheme = "https",
                    domain = "example.org"
                },
                new
                {
                    scheme = new SchemeRouteConstraint(),
                    domain = new DomainRouteConstraint(),
                    language = new ChoiceRouteConstraint(Localization.Languages)
                });
        }

        #endregion
    }
}
