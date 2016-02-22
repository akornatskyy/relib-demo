using ReusableLibrary.Abstractions.Tracing;

namespace Tickets.Interface.Instrumentation
{
    public sealed class MembershipInstrumentationProvider
    {
        private readonly IPerformanceCounter authenticationCounter;
        private readonly IPerformanceCounter authenticationFailedCounter;

        public MembershipInstrumentationProvider(bool enabled)
        {
            var factory = new PerformanceCounterFactory(InstrumentationNames.DemoMembershipCategory, InstrumentationNames.InstanceSuffix, enabled);
            authenticationCounter = factory.Create("Authentication Requests/sec", "Total Authentication Requests");
            authenticationFailedCounter = factory.Create("Authentication Failed Requests/sec", "Total Authentication Failed Requests");
        }

        public void FireAuthentication(bool succeed)
        {
            authenticationCounter.Increment();
            if (!succeed)
            {
                authenticationFailedCounter.Increment();
            }
        }
    }
}