using System.Globalization;

using ReusableLibrary.Abstractions.Repository;

using Tickets.Interface.Models;

namespace Tickets.Interface.Repository.Caching.Keys
{
    public static class TicketKeyHelper
    {
        public const string Prefix = "T:";

        public static string Retrieve(int identity)
        {
            return string.Concat(Prefix, identity.ToString(CultureInfo.InvariantCulture));
        }

        public static string RetrieveMultiple(RetrieveMultipleRequest<TicketSpecification> request)
        {
            return string.Concat(Prefix, request.ToKeyString());
        }

        public static string TicketDependency()
        {
            return Prefix + "Dependency";
        }
    }
}