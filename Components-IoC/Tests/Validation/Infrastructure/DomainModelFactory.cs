using System;
using System.Collections;
using ReusableLibrary.Supplemental.Collections;
using ReusableLibrary.Supplemental.System;
using Tickets.Interface.Models;

namespace Public.ValidationTests.Infrastructure
{
    public static class DomainModelFactory
    {
        private static readonly Random g_random = new Random();
        private static readonly IDictionary g_factoryMethods = new Hashtable();

        static DomainModelFactory()
        {
            g_factoryMethods.Add("Tickets.Interface.Models.UserCredentials, Tickets.Interface", new Func<UserCredentials>(UserCredentials));
            g_factoryMethods.Add("Tickets.Interface.Models.UserInfo, Tickets.Interface", new Func<UserInfo>(UserInfo));
        }

        public static object Create(string name)
        {
            Delegate result;
            if (!g_factoryMethods.TryGetValue<Delegate>(name, out result))
            {
                throw new ArgumentException("Unable find factory method for '{0}'".FormatWith(name), "name");
            }

            return result.DynamicInvoke();
        }

        private static string Mail()
        {
            return "{0}@{1}.com".FormatWith(g_random.NextWord(), g_random.NextWord());
        }

        private static UserCredentials UserCredentials()
        {
            return new UserCredentials() 
            { 
                UserName = g_random.NextWord(),
                Password = "P@ssw0rd"
            };
        }

        private static UserInfo UserInfo()
        {
            return new UserInfo()
            {
                DisplayName = g_random.NextWord(),
                Email = Mail()
            };
        }
    }
}
