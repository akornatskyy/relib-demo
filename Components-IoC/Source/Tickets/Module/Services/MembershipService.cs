using System.Collections.Generic;
using System.Linq;

using ReusableLibrary.Abstractions.Models;
using ReusableLibrary.Abstractions.Services;

using Tickets.Interface.Models;
using Tickets.Interface.Repository;
using Tickets.Interface.Services;

namespace Tickets.Module.Services
{
    public sealed class MembershipService : AbstractService, IMembershipService
    {
        private readonly IMembershipRepository membershipRepository;
        private readonly ILazy<IEnumerable<PasswordQuestion>> lazyPasswordQuestions;
        private readonly ILazy<IDictionary<int, PasswordQuestion>> lazyPasswordQuestionMap;

        public MembershipService(IMembershipRepository membershipRepository)
        {
            this.membershipRepository = membershipRepository;
            lazyPasswordQuestions = new LazyObject<IEnumerable<PasswordQuestion>>(
                () => this.membershipRepository.ListPasswordQuestions(CurrentCulture.TwoLetterISOLanguageName));
            lazyPasswordQuestionMap = new LazyObject<IDictionary<int, PasswordQuestion>>(
                () => this.membershipRepository.MapPasswordQuestion(CurrentCulture.TwoLetterISOLanguageName));
        }

        #region IMembershipService Members

        public bool ValidateUser(UserCredentials user)
        {
            return WithValid(user, () =>
            {
                membershipRepository.ValidateUser(user);
            });
        }

        public bool CreateUser(UserSignUpInfo signUpInfo)
        {
            return WithValid(signUpInfo, () => membershipRepository.CreateUser(signUpInfo));
        }

        public PasswordQuestion DefaultPasswordQuestion
        {
            get { return PasswordQuestions.First(); }
        }

        public IEnumerable<PasswordQuestion> PasswordQuestions
        {
            get { return lazyPasswordQuestions.Object; }
        }

        public IDictionary<int, PasswordQuestion> PasswordQuestionMap
        {
            get { return lazyPasswordQuestionMap.Object; }
        }

        #endregion
    }
}