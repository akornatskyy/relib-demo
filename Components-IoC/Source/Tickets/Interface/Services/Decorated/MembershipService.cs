using System.Collections.Generic;
using Tickets.Interface.Models;

namespace Tickets.Interface.Services.Decorated
{
    public abstract class MembershipService : IMembershipService
    {
        private readonly IMembershipService m_inner;

        public MembershipService(IMembershipService inner)
        {
            m_inner = inner;
        }

        #region IMembershipService Members

        public virtual bool ValidateUser(UserCredentials credentials)
        {
            return m_inner.ValidateUser(credentials);
        }

        public virtual bool CreateUser(UserSignUpInfo signUpInfo)
        {
            return m_inner.CreateUser(signUpInfo);
        }

        public virtual PasswordQuestion DefaultPasswordQuestion
        {
            get { return m_inner.DefaultPasswordQuestion; }
        }

        public virtual IEnumerable<PasswordQuestion> PasswordQuestions
        {
            get { return m_inner.PasswordQuestions; }
        }

        public virtual IDictionary<int, PasswordQuestion> PasswordQuestionMap
        {
            get { return m_inner.PasswordQuestionMap; }
        }

        #endregion
    }
}
