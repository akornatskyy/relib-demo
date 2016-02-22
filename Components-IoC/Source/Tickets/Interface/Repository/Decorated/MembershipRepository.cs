using System.Collections.Generic;
using System.Diagnostics;

using Tickets.Interface.Models;

namespace Tickets.Interface.Repository.Decorated
{
    public abstract class MembershipRepository : IMembershipRepository
    {
        private readonly IMembershipRepository innerRepository;

        protected MembershipRepository(IMembershipRepository innerRepository)
        {
            this.innerRepository = innerRepository;
        }

        #region IMembershipRepository Members

        [DebuggerStepThrough]
        public void ValidateUser(UserCredentials userCredentials)
        {
            innerRepository.ValidateUser(userCredentials);
        }

        [DebuggerStepThrough]
        public void CreateUser(UserSignUpInfo userSignUpInfo)
        {
            innerRepository.CreateUser(userSignUpInfo);
        }

        [DebuggerStepThrough]
        public virtual IEnumerable<PasswordQuestion> ListPasswordQuestions(string languageCode)
        {
            return innerRepository.ListPasswordQuestions(languageCode);
        }

        [DebuggerStepThrough]
        public virtual IDictionary<int, PasswordQuestion> MapPasswordQuestion(string languageCode)
        {
            return innerRepository.MapPasswordQuestion(languageCode);
        }

        #endregion

        #region IRetrieveRepository<int,Staff> Members

        [DebuggerStepThrough]
        public virtual Staff Retrieve(int identity)
        {
            return innerRepository.Retrieve(identity);
        }

        #endregion
    }
}