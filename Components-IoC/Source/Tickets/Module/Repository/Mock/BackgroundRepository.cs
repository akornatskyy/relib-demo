using System;
using ReusableLibrary.Supplemental.System;
using Tickets.Interface.Repository;

namespace Tickets.Module.Repository.Mock
{
    public sealed class BackgroundRepository : IBackgroundRepository
    {
        private readonly Random m_random;

        public BackgroundRepository()
        {
            m_random = new Random();
        }

        #region IBackgroundRepository Members

        public int AcquireOverdueTicket()
        {
            return m_random.NextInt(0, 50);
        }

        #endregion
    }
}
