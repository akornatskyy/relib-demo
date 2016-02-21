using System.Collections.Generic;
using System.Web.Mvc;

using ReusableLibrary.Web.Mvc;

using Tickets.Interface.Models;

namespace Public.WebMvc.Models
{
    public sealed class TicketSearchViewData : SearchViewData<TicketSpecification>
    {
        public IEnumerable<SelectListItem> TicketTypeSelectList { get; set; }
    }
}