using System.Collections.Generic;

using ReusableLibrary.Abstractions.Repository;

using Tickets.Interface.Models;

namespace Tickets.Interface.Repository
{
    public interface IMembershipRepository : IRetrieveRepository<int, Staff>
    {
        void ValidateUser(UserCredentials userCredentials);

        void CreateUser(UserSignUpInfo userSignUpInfo);

        IEnumerable<PasswordQuestion> ListPasswordQuestions(string languageCode);

        IDictionary<int, PasswordQuestion> MapPasswordQuestion(string languageCode);
    }
}