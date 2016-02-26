using Xunit;
using Xunit.Extensions;

using Public.ValidationTests.Constants;
using Public.ValidationTests.Helpers;

namespace Public.ValidationTests.Fixtures
{
    public sealed class TicketValidationTest
    {
        [Theory]
        [Trait(TraitNames.TicketValidation, "UserCredentials")]
        [ExcelData("ValidationTestCases.xls", @"SELECT * FROM TestRange WHERE         
        Name LIKE '%UserCredentials%' AND Property='UserName'")]
        public void UserCredentials_UserName(string name, string ruleSet, string property, string data, bool succeed, string error)
        {
            ValidationHelper.Validate(name, ruleSet, property, data ?? string.Empty, succeed, error);
        }

        [Theory]
        [Trait(TraitNames.TicketValidation, "UserCredentials")]
        [ExcelData("ValidationTestCases.xls", @"SELECT * FROM TestRange WHERE         
        Name LIKE '%UserCredentials%' AND Property='Password'")]
        public void UserCredentials_Password(string name, string ruleSet, string property, string data, bool succeed, string error)
        {
            ValidationHelper.Validate(name, ruleSet, property, data ?? string.Empty, succeed, error);
        }

        [Theory]
        [Trait(TraitNames.TicketValidation, "UserInfo")]
        [ExcelData("ValidationTestCases.xls", @"SELECT * FROM TestRange WHERE         
        Name LIKE '%UserInfo%' AND Property='DisplayName'")]
        public void UserInfo_DisplayName(string name, string ruleSet, string property, string data, bool succeed, string error)
        {
            ValidationHelper.Validate(name, ruleSet, property, data ?? string.Empty, succeed, error);
        }
    }
}