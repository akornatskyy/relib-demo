using System;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Xunit;

using Public.ValidationTests.Constants;
using Public.ValidationTests.Infrastructure;

namespace Public.ValidationTests.Helpers
{
    public static class ValidationHelper
    {
        public static void Validate(string name, string ruleSet, string property, object data, bool succeed, string error)
        {
            // Arrange
            var validator = ValidationFactory.CreateValidator(Type.GetType(name), ruleSet, Configuration.Source);
            var model = DomainModelFactory.Create(name);
            model.GetType().GetProperty(property).GetSetMethod().Invoke(model, new[] { data });

            // Act            
            var results = validator.Validate(model);

            // Assert
            if (succeed)
            {
                Assert.Equal(0, results.Count);
            }
            else
            {
                AssertHelper.Exists(results, property, error);
            }
        }
    }
}