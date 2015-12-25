using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReusableLibrary.Abstractions.Repository;

namespace Tickets.Interface.WorkItem.Rules
{
    public static class RetrieveTicketExtensions
    {
        public static bool RetrieveTicket(this EscalateOverdueTicketWorkItem workItem)
        {
            try
            {
                var item = workItem.TicketRepository.Retrieve(workItem.StateBag.Number);
                workItem.StateBag.Item = item;
            }
            catch (RepositoryException rex)
            {
                workItem.ReportError(rex.Message);
            }

            return workItem.StateBag.Item != null;
        }
    }
}
