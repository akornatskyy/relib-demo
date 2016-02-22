using Tickets.Interface.Instrumentation;
using Tickets.Interface.Models;

namespace Tickets.Interface.Services.Audit
{
    public sealed class MembershipService : Decorated.MembershipService
    {
        private readonly MembershipInstrumentationProvider instrumentationProvider;

        public MembershipService(MembershipInstrumentationProvider instrumentationProvider, IMembershipService inner)
            : base(inner)
        {
            this.instrumentationProvider = instrumentationProvider;
        }

        public override bool ValidateUser(UserCredentials credentials)
        {
            bool succeed = base.ValidateUser(credentials);

            instrumentationProvider.FireAuthentication(succeed);
            
            return succeed;
        }
    }
}