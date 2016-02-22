using System.Collections.Generic;

using Tickets.Interface.Models;

namespace Tickets.Interface.Services.Decorated
{
    public abstract class MembershipService : IMembershipService
    {
        private readonly IMembershipService inner;

        protected MembershipService(IMembershipService inner)
        {
            this.inner = inner;
        }

        #region IMembershipService Members

        public virtual bool ValidateUser(UserCredentials credentials)
        {
            return inner.ValidateUser(credentials);
        }

        public virtual bool CreateUser(UserSignUpInfo signUpInfo)
        {
            return inner.CreateUser(signUpInfo);
        }

        public virtual PasswordQuestion DefaultPasswordQuestion
        {
            get { return inner.DefaultPasswordQuestion; }
        }

        public virtual IEnumerable<PasswordQuestion> PasswordQuestions
        {
            get { return inner.PasswordQuestions; }
        }

        public virtual IDictionary<int, PasswordQuestion> PasswordQuestionMap
        {
            get { return inner.PasswordQuestionMap; }
        }

        #endregion
    }
}