using ReusableLibrary.Abstractions.Tracing;

namespace Tickets.Interface.Instrumentation
{
    public sealed class MembershipInstrumentationProvider
    {
        private readonly IPerformanceCounter m_authenticationCounter;
        private readonly IPerformanceCounter m_authenticationFailedCounter;

        public MembershipInstrumentationProvider(bool enabled)
        {
            var factory = new PerformanceCounterFactory(InstrumentationNames.DemoMembershipCategory, InstrumentationNames.InstanceSuffix, enabled);
            m_authenticationCounter = factory.Create("Authentication Requests/sec", "Total Authentication Requests");
            m_authenticationFailedCounter = factory.Create("Authentication Failed Requests/sec", "Total Authentication Failed Requests");
        }

        public void FireAuthentication(bool succeed)
        {
            m_authenticationCounter.Increment();
            if (!succeed)
            {
                m_authenticationFailedCounter.Increment();
            }
        }
    }
}
