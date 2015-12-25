using System.Data.Linq;
using ReusableLibrary.Abstractions.Repository;

namespace Tickets.Module.Repository
{
    public partial class TicketsDataContext
    {
        public TicketsDataContext(DbConnectionStringProvider provider)
            : this((provider ?? new DbConnectionStringProvider("ApplicationServices")).ConnectionString)
        {
            var options = new DataLoadOptions();
            LoadOptions = options;
        }
    }
}
