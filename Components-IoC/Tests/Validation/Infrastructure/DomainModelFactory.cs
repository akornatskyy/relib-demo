using System;
using System.Collections;

using ReusableLibrary.Supplemental.Collections;
using ReusableLibrary.Supplemental.System;

using Tickets.Interface.Models;

namespace Public.ValidationTests.Infrastructure
{
    public static class DomainModelFactory
    {
        private static readonly Random Rnd = new Random();
        private static readonly IDictionary FactoryMethods = new Hashtable();

        static DomainModelFactory()
        {
            FactoryMethods.Add("Tickets.Interface.Models.UserCredentials, Tickets.Interface", new Func<UserCredentials>(UserCredentials));
            FactoryMethods.Add("Tickets.Interface.Models.UserInfo, Tickets.Interface", new Func<UserInfo>(UserInfo));
        }

        public static object Create(string name)
        {
            Delegate result;
            if (!FactoryMethods.TryGetValue(name, out result))
            {
                throw new ArgumentException("Unable find factory method for '{0}'".FormatWith(name), "name");
            }

            return result.DynamicInvoke();
        }

        private static string Mail()
        {
            return "{0}@{1}.com".FormatWith(Rnd.NextWord(), Rnd.NextWord());
        }

        private static UserCredentials UserCredentials()
        {
            return new UserCredentials() 
            { 
                UserName = Rnd.NextWord(),
                Password = "P@ssw0rd"
            };
        }

        private static UserInfo UserInfo()
        {
            return new UserInfo()
            {
                DisplayName = Rnd.NextWord(),
                Email = Mail()
            };
        }
    }
}