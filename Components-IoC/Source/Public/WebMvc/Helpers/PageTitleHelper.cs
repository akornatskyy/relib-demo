using System.Web.Mvc;

using ReusableLibrary.Supplemental.System;

namespace Public.WebMvc.Helpers
{
    public static class PageTitleHelper
    {
        public static string PageTitle(this HtmlHelper helper, string title)
        {
            return helper.Encode(Properties.Resources.FormatPageTitle.FormatWith(title));
        }
    }
}