using ReusableLibrary.Captcha;
using ReusableLibrary.Web.Mvc;

using Tickets.Interface.Models;

namespace Public.WebMvc.Models
{
    public sealed class LogonViewData : DetailsViewData<UserCredentials>, 
        ITuringNumber,
        IPartialViewNameProvider
    {
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        #region ITuringNumber Members

        public string TuringNumber { get; set; }

        #endregion

        #region IPartialViewNameProvider Members

        public string PartialViewName
        {
            get { return "Authenticate"; }
        }

        #endregion
    }
}