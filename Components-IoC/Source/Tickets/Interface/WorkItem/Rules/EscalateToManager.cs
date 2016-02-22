namespace Tickets.Interface.WorkItem.Rules
{
    public static class EscalateToManagerExtensions 
    {
        public static bool EscalateToManager(this EscalateOverdueTicketWorkItem workItem)
        {
            var staff = workItem.MembershipRepository.Retrieve(workItem.StateBag.Item.AssignedToId);
            workItem.StateBag.Item.AssignedToId = staff.ReportsTo;
            return staff.ReportsTo > 0;
        }
    }
}