using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tickets.Interface.Repository
{
    public interface IBackgroundRepository
    {
        int AcquireOverdueTicket();
    }
}
