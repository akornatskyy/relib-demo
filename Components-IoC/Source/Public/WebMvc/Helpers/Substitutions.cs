using System;
using System.Web;
using System.Web.Routing;
using ReusableLibrary.Web.Mvc.Integration;

namespace Public.WebMvc.Helpers
{
    public static class Substitutions
    {
        public const string CurrentTime = "__CurrentTime__";

        public static string StartCallback(HttpContextBase context, object state)
        {
            var routeValues = state as RouteValueDictionary;
            if (routeValues != null)
            {
                LocalizationControllerFactory.TryLocalizeContext(routeValues);
            }

            return string.Empty;
        }

        public static string CurrentTimeCallback(HttpContextBase context, object state)
        {
            return DateTime.Now.ToString();
        }
    }
}
