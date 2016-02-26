using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using ReusableLibrary.Supplemental.System;
using Xunit;

namespace Public.ValidationTests.Helpers
{
    public static class AssertHelper
    {
        public static void Exists(ValidationResults results, string key, string error)
        {
            var result = results.FirstOrDefault(r => r.Key.Equals(key)
                && r.Message.Equals(error)) != null;

            if (!result)
            {
                Assert.True(false, "Validation Rule has no match. Key = '{0}', Expected = '{1}'.\r\nRules Failed:\r\n{2}"
                    .FormatWith(key, error, Dump(results)));
            }
        }

        private static string Dump(ValidationResults results)
        {
            var buffer = new StringBuilder();
            results.ToList().ForEach(r => buffer.AppendLine("{0} => '{1}'".FormatWith(r.Key, r.Message)));
            return buffer.ToString();
        }
    }
}