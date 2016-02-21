using System.Web.Routing;

using ReusableLibrary.Abstractions.Bootstrapper;
using ReusableLibrary.Web.Mvc.Integration;
using ReusableLibrary.Web.Mvc.Routing;
using ReusableLibrary.Web.Routing;

namespace Public.WebMvc.Helpers
{
    public sealed class RegisterSubstitutions : IStartupTask
    {
        #region IStartupTask Members

        public void Execute()
        {
            AddSubstitutions();
            AddRouting();
        }

        #endregion

        private void AddSubstitutions()
        {
            HttpResponseSubstitutionHandler
                .Add(Substitutions.CurrentTime, Substitutions.CurrentTimeCallback);
        }

        private void AddRouting()
        {
            var handler = new HttpResponseSubstitutionRouteHandler()
            {
                StateProvider = SubstitutionStateProvider.RouteValues,
                StartCallback = Substitutions.StartCallback
            };

            var routes = RouteTable.Routes;
            routes.MapRoute("cache-substitution", "{language}/cache-substitution",
                new
                {
                    controller = "Home",
                    action = "CacheSubstitution",
                    id = string.Empty,
                    language = Localization.DefaultLanguage,
                }, handler);
        }
    }
}