using System.Collections.Generic;
using System.Web.Mvc;
using ReusableLibrary.Web.Mvc;
using Tickets.Interface.Models;

namespace Public.WebMvc.Models
{
    public sealed class RegisterViewData : DetailsViewData<UserSignUpInfo>
    {
        public string Password 
        {
            get { return Details.Credentials.Password; }
        }

        public string ConfirmPassword { get; set; }

        public int QuestionId { get; set; }

        public IEnumerable<SelectListItem> PasswordQuestionSelectList { get; set; }
    }
}
