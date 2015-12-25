using ReusableLibrary.Abstractions.Models;
using ReusableLibrary.Abstractions.Tracing;
using ReusableLibrary.Abstractions.WorkItem;
using Tickets.Interface.Repository;
using Tickets.Interface.WorkItem.Rules;
using Tickets.Interface.Models;
using System;

namespace Tickets.Interface.WorkItem
{
    public sealed class EscalateOverdueTicketWorkItem : AbstractWorkItem<TicketStateBag>
    {
        public EscalateOverdueTicketWorkItem()
        {
            Rules = new Func2<bool>[] 
            { 
                this.RetrieveTicket,
                this.EscalateToManager
            };
        }

        public IBackgroundRepository BackgroundRepository { get; set; }

        public ITicketRepository TicketRepository { get; set; }

        public IMembershipRepository MembershipRepository { get; set; }

        public override bool OnAcquireUnitOfWork()
        {
            StateBag.Number = BackgroundRepository.AcquireOverdueTicket();
            if (StateBag.Number <= 0)
            {
                return false;
            }

            if (TraceInfo.IsInfoEnabled)
            {
                TraceHelper.TraceInfo(TraceInfo, "Number = '{0}'", StateBag.Number);
            }

            return true;
        }

        public override void OnAllRulesSatisfied()
        {
            var ticket = StateBag.Item;
            TicketRepository.UpdateTicket("background", ticket);
            TicketRepository.AddHistory("background", new TicketHistory() 
            { 
                TicketId = ticket.TicketId,
                StaffId = ticket.AssignedToId,
                CreatedOn = DateTime.Now,
                Comment = "The ticket has been automatically reassigned according to overdue ticket escalation policy"
            });
        }
    }
}
