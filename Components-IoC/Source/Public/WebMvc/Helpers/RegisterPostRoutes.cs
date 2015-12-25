using System.Web.Mvc;
using System.Web.Routing;
using ReusableLibrary.Abstractions.Bootstrapper;
using ReusableLibrary.Web.Mvc.Integration;

namespace Public.WebMvc.Helpers
{
    public sealed class RegisterPostRoutes : IStartupTask
    {
        #region IStartupTask Members

        public void Execute()
        {
            RouteTable.Routes.MapRoute("Logon", "account/logon",
                new { controller = "Account", action = "Logon", language = Localization.DefaultLanguage });
        }

        #endregion
    }
}
