using System.Collections.Generic;
using System.Diagnostics;
using Tickets.Interface.Models;

namespace Tickets.Interface.Repository.Decorated
{
    public abstract class MembershipRepository : IMembershipRepository
    {
        private IMembershipRepository m_innerRepository;

        protected MembershipRepository(IMembershipRepository innerRepository)
        {
            m_innerRepository = innerRepository;
        }

        #region IMembershipRepository Members

        [DebuggerStepThrough]
        public void ValidateUser(UserCredentials userCredentials)
        {
            m_innerRepository.ValidateUser(userCredentials);
        }

        [DebuggerStepThrough]
        public void CreateUser(UserSignUpInfo userSignUpInfo)
        {
            m_innerRepository.CreateUser(userSignUpInfo);
        }

        [DebuggerStepThrough]
        public virtual IEnumerable<PasswordQuestion> ListPasswordQuestions(string languageCode)
        {
            return m_innerRepository.ListPasswordQuestions(languageCode);
        }

        [DebuggerStepThrough]
        public virtual IDictionary<int, PasswordQuestion> MapPasswordQuestion(string languageCode)
        {
            return m_innerRepository.MapPasswordQuestion(languageCode);
        }

        #endregion

        #region IRetrieveRepository<int,Staff> Members

        [DebuggerStepThrough]
        public virtual Staff Retrieve(int identity)
        {
            return m_innerRepository.Retrieve(identity);
        }

        #endregion
    }
}
