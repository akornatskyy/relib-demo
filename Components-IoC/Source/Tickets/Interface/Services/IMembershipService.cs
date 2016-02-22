using System.Collections.Generic;

using Tickets.Interface.Models;

namespace Tickets.Interface.Services
{
    public interface IMembershipService
    {
        bool ValidateUser(UserCredentials credentials);

        bool CreateUser(UserSignUpInfo signUpInfo);

        PasswordQuestion DefaultPasswordQuestion { get; }

        IEnumerable<PasswordQuestion> PasswordQuestions { get; }

        IDictionary<int, PasswordQuestion> PasswordQuestionMap { get; }
    }
}