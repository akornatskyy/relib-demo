using System;

using ReusableLibrary.Supplemental.System;

using Tickets.Interface.Repository;

namespace Tickets.Module.Repository.Mock
{
    public sealed class BackgroundRepository : IBackgroundRepository
    {
        private readonly Random random;

        public BackgroundRepository()
        {
            random = new Random();
        }

        #region IBackgroundRepository Members

        public int AcquireOverdueTicket()
        {
            return random.NextInt(0, 50);
        }

        #endregion
    }
}