using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Public.ValidationTests.Infrastructure;
using Xunit;

namespace Public.ValidationTests.Helpers
{
    public static class ValidationHelper
    {
        public static void Validate(string name, string ruleSet, string property, object data, bool succeed, string error)
        {
            // Arrange
            var validator = ValidationFactory.CreateValidator(Type.GetType(name), ruleSet, Constants.Configuration.Source);
            var model = DomainModelFactory.Create(name);
            model.GetType().GetProperty(property).GetSetMethod().Invoke(model, new object[] { data });

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
