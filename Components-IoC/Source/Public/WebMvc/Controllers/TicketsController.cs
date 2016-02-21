using System.Web.Mvc;

using ReusableLibrary.Abstractions.Services;
using ReusableLibrary.Web.Mvc;

using Public.WebMvc.Constants;
using Public.WebMvc.Models;
using Tickets.Interface.Models;
using Tickets.Interface.Services;

namespace Public.WebMvc.Controllers
{
    [Authorize, OutputCache(CacheProfile = CacheProfileNames.PrivateContent)]
    public class TicketsController : AbstractController
    {
        private readonly IValidationService validationService;
        private readonly IMementoService mementoService;
        private readonly ITicketService ticketService;

        public TicketsController(IValidationService validationService,
            IMementoService mementoService,
            ITicketService ticketService)
        {
            this.validationService = validationService;
            this.mementoService = mementoService;
            this.ticketService = ticketService;
        }

        [HttpGet,
        /*OutputCache(VaryByCustom = Constants.VaryByCustomNames.Tickets, 
            CacheProfile = Constants.CacheProfileNames.StaticPerUserContent),*/
        OutputCache(CacheProfile = CacheProfileNames.PrivateContent)]
        public ActionResult Index()
        {
            // If you want to use MementoService set output cache to use
            // PrivateContent cache profile.
            // If your preference is better caching, than StaticPerUserContent.
            return View(AddViewDataLists(new TicketSearchViewData() 
            {
                Specification = mementoService.Load<TicketSpecification>()
            }));
        }

        [HttpGet]
        public ActionResult Search()
        {
            var viewData = new TicketSearchViewData();
            if (!TryUpdateModel(viewData.Specification)
                | !validationService.Validate(viewData.Specification)
                || !mementoService.Save(viewData.Specification))
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
                Details = ticketService.LoadTicket(id)
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
                Items = ticketService.SearchTickets(specification, pageIndex, pageSize)
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
                viewData.Specification.TicketTypeId = ticketService.DefaultTicketType.Key;
            }

            viewData.TicketTypeSelectList = ticketService.TicketTypes.ToSelectList(viewData.Specification.TicketTypeId);
            return viewData;
        }
    }
}