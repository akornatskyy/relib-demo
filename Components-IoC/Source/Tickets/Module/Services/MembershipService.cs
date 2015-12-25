using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using ReusableLibrary.Abstractions.Models;
using ReusableLibrary.Abstractions.Services;
using Tickets.Interface.Models;
using Tickets.Interface.Repository;
using Tickets.Interface.Services;

namespace Tickets.Module.Services
{
    public sealed class MembershipService : AbstractService, IMembershipService
    {
        private readonly IMembershipRepository m_membershipRepository;
        private readonly ILazy<IEnumerable<PasswordQuestion>> m_lazyPasswordQuestions;
        private readonly ILazy<IDictionary<int, PasswordQuestion>> m_lazyPasswordQuestionMap;

        public MembershipService(IMembershipRepository membershipRepository)
        {
            m_membershipRepository = membershipRepository;
            m_lazyPasswordQuestions = new LazyObject<IEnumerable<PasswordQuestion>>(
                () => m_membershipRepository.ListPasswordQuestions(CurrentCulture.TwoLetterISOLanguageName));
            m_lazyPasswordQuestionMap = new LazyObject<IDictionary<int, PasswordQuestion>>(
                () => m_membershipRepository.MapPasswordQuestion(CurrentCulture.TwoLetterISOLanguageName));
        }

        #region IMembershipService Members

        public bool ValidateUser(UserCredentials user)
        {
            return WithValid(user, () =>
            {
                m_membershipRepository.ValidateUser(user);
            });
        }

        public bool CreateUser(UserSignUpInfo signUpInfo)
        {
            return WithValid(signUpInfo, () => m_membershipRepository.CreateUser(signUpInfo));
        }

        public PasswordQuestion DefaultPasswordQuestion
        {
            get { return PasswordQuestions.First(); }
        }

        public IEnumerable<PasswordQuestion> PasswordQuestions
        {
            get { return m_lazyPasswordQuestions.Object; }
        }

        public IDictionary<int, PasswordQuestion> PasswordQuestionMap
        {
            get { return m_lazyPasswordQuestionMap.Object; }
        }

        #endregion
    }
}
