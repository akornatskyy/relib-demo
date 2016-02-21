using ReusableLibrary.Web.Mvc;

using Tickets.Interface.Models;

namespace Public.WebMvc.Models
{
    public class TicketListViewData
         : ListViewData<TicketSpecification, TicketSearchResult>,
        IPartialViewNameProvider
    {
        #region IPartialViewNameProvider Members

        public string PartialViewName
        {
            get { return "ListItems"; }
        }

        #endregion
    }
}