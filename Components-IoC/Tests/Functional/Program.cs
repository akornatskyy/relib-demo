using System;
using System.Linq.Expressions;
using Public.FunctionalTests.Fixtures;
using WatiN.Core.Logging;
using Xunit;

namespace Public.FunctionalTests
{
    public static class Program
    {
        [STAThread]
        internal static void Main()
        {
            Run<AccountTest>(fixture => fixture.Authenticate());
        }

        private static void Run<TFixture>(Expression<Action<TFixture>> expression)
            where TFixture : IUseFixture<DefaultLifeTimeContainer>, new()
        {
            try
            {
                Logger.LogWriter = new ConsoleLogWriter();
                var test = new TFixture();
                using (var lifeTimeContainer = new DefaultLifeTimeContainer())
                {
                    test.SetFixture(lifeTimeContainer);
                    var action = expression.Compile();
                    action(test);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(ex);
            }

            Environment.Exit(0);
        }
    }
}
