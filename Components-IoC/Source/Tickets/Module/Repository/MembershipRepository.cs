using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using ReusableLibrary.Abstractions.Repository;
using ReusableLibrary.Supplemental.Collections;
using Tickets.Interface.Models;
using Tickets.Interface.Repository;
using Tickets.Module.Helpers;

namespace Tickets.Module.Repository
{
    public sealed partial class MembershipRepository : IMembershipRepository
    {
        private readonly TicketsDataContext m_context;
        private readonly MembershipProvider m_provider;

        public MembershipRepository(TicketsDataContext context)
            : this(context, null)
        {
        }

        private MembershipRepository(TicketsDataContext context, MembershipProvider provider)
        {
            m_context = context;
            m_provider = provider ?? Membership.Provider;
        }

        #region IMembershipRepository Members

        public void ValidateUser(UserCredentials credentials)
        {
            if (!m_provider.ValidateUser(credentials.UserName, credentials.Password))
            {
                throw new RepositoryGuardException(Properties.Resources.GuardValidateUser);
            }
        }

        public void CreateUser(UserSignUpInfo signUpInfo)
        {
            MembershipCreateStatus status;
            var user = m_provider.CreateUser(
                signUpInfo.Credentials.UserName, 
                signUpInfo.Credentials.Password, 
                signUpInfo.Info.Email, 
                signUpInfo.Question.DisplayName, 
                signUpInfo.Answer, true, null, out status);
            if (status != MembershipCreateStatus.Success)
            {
                throw new RepositoryFailureException(MembershipCreateStatusHelper.ErrorCodeToString(status));
            }

            var staff = new Entities.Staff()
            {
                DisplayName = signUpInfo.Info.DisplayName,
                UserId = (Guid)user.ProviderUserKey,
                StaffType = 'T'
            };

            m_context.Staffs.InsertOnSubmit(staff);
        }

        public IEnumerable<PasswordQuestion> ListPasswordQuestions(string languageCode)
        {
            return (FindAllPasswordQuestionOrderedByNameQuery.Invoke(m_context, languageCode))
                    .ToList().AsReadOnly();
        }

        public IDictionary<int, PasswordQuestion> MapPasswordQuestion(string languageCode)
        {
            return ListPasswordQuestions(languageCode).ToDictionary<int, PasswordQuestion>();
        }

        #endregion

        #region IRetrieveRepository<int,Staff> Members

        public Staff Retrieve(int identity)
        {
            var s = FindByIdQuery.Invoke(m_context, identity);
            return new Staff()
            {
                StaffId = s.StaffId,
                DisplayName = s.DisplayName,
                ReportsTo = s.ReportsToId ?? 0
            };
        }

        #endregion
    }
}
