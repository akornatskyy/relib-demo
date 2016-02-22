namespace Tickets.Interface.Repository
{
    public interface IBackgroundRepository
    {
        int AcquireOverdueTicket();
    }
}