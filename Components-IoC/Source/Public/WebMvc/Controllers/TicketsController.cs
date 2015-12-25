using System.Web.Mvc;
using Public.WebMvc.Models;
using ReusableLibrary.Abstractions.Services;
using ReusableLibrary.Web.Mvc;
using Tickets.Interface.Models;
using Tickets.Interface.Services;

namespace Public.WebMvc.Controllers
{
    [Authorize,
    OutputCache(CacheProfile = Constants.CacheProfileNames.PrivateContent)]
    public class TicketsController : AbstractController
    {
        private readonly IValidationService m_validationService;
        private readonly IMementoService m_mementoService;
        private readonly ITicketService m_ticketService;

        public TicketsController(IValidationService validationService,
            IMementoService mementoService,
            ITicketService ticketService)
        {
            m_validationService = validationService;
            m_mementoService = mementoService;
            m_ticketService = ticketService;
        }

        [HttpGet,
        /*OutputCache(VaryByCustom = Constants.VaryByCustomNames.Tickets, 
            CacheProfile = Constants.CacheProfileNames.StaticPerUserContent),*/
        OutputCache(CacheProfile = Constants.CacheProfileNames.PrivateContent)]
        public ActionResult Index()
        {
            // If you want to use MementoService set output cache to use
            // PrivateContent cache profile.
            // If your preference is better caching, than StaticPerUserContent.
            return View(AddViewDataLists(new TicketSearchViewData() 
            {
                Specification = m_mementoService.Load<TicketSpecification>()
            }));
        }

        [HttpGet]
        public ActionResult Search()
        {
            var viewData = new TicketSearchViewData();
            if (!TryUpdateModel(viewData.Specification)
                | !m_validationService.Validate(viewData.Specification)
                || !m_mementoService.Save(viewData.Specification))
            {
                AddViewDataLists(viewData);
                return View(viewData);
            }

            return List(viewData.Specification, null, null);
        }

        [HttpGet]
        public ActionResult List(int? pageIndex, int? pageSize)
        {
            var model = new TicketSpecification();
            if (!TryUpdateModel(model))
            {
                return new BadRequestResult();
            }

            return List(model, pageIndex, pageSize);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var viewData = new TicketDetailsViewData()
            {
                Details = m_ticketService.LoadTicket(id)
            };
            if (viewData.Details == null)
            {
                return new FileNotFoundResult();
            }

            return View(viewData);
        }

        private ActionResult List(TicketSpecification specification, int? pageIndex, int? pageSize)
        {
            var viewData = new TicketListViewData()
            {
                Specification = specification,
                Items = m_ticketService.SearchTickets(specification, pageIndex, pageSize)
            };
            if (viewData.Items == null)
            {
                return new BadRequestResult();
            }

            return AlternatePartialView("List", viewData);
        }

        private TicketSearchViewData AddViewDataLists(TicketSearchViewData viewData)
        {
            if (viewData.Specification.TicketTypeId == 0)
            {
                viewData.Specification.TicketTypeId = m_ticketService.DefaultTicketType.Key;
            }

            viewData.TicketTypeSelectList = m_ticketService.TicketTypes.ToSelectList(viewData.Specification.TicketTypeId);
            return viewData;
        }
    }
}
