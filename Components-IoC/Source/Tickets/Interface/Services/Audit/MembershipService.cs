using Tickets.Interface.Instrumentation;
using Tickets.Interface.Models;

namespace Tickets.Interface.Services.Audit
{
    public sealed class MembershipService : Decorated.MembershipService
    {
        private readonly MembershipInstrumentationProvider m_instrumentationProvider;

        public MembershipService(MembershipInstrumentationProvider instrumentationProvider, IMembershipService inner)
            : base(inner)
        {
            m_instrumentationProvider = instrumentationProvider;
        }

        public override bool ValidateUser(UserCredentials credentials)
        {
            bool succeed = base.ValidateUser(credentials);

            m_instrumentationProvider.FireAuthentication(succeed);
            
            return succeed;
        }
    }
}
