using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

using ReusableLibrary.Abstractions.Repository;
using ReusableLibrary.Supplemental.Collections;

using Tickets.Interface.Models;
using Tickets.Interface.Repository;
using Tickets.Module.Helpers;
using Tickets.Module.Properties;

namespace Tickets.Module.Repository
{
    public sealed partial class MembershipRepository : IMembershipRepository
    {
        private readonly TicketsDataContext context;
        private readonly MembershipProvider provider;

        public MembershipRepository(TicketsDataContext context)
            : this(context, null)
        {
        }

        private MembershipRepository(TicketsDataContext context, MembershipProvider provider)
        {
            this.context = context;
            this.provider = provider ?? Membership.Provider;
        }

        #region IMembershipRepository Members

        public void ValidateUser(UserCredentials credentials)
        {
            if (!provider.ValidateUser(credentials.UserName, credentials.Password))
            {
                throw new RepositoryGuardException(Resources.GuardValidateUser);
            }
        }

        public void CreateUser(UserSignUpInfo signUpInfo)
        {
            MembershipCreateStatus status;
            var user = provider.CreateUser(
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

            context.Staffs.InsertOnSubmit(staff);
        }

        public IEnumerable<PasswordQuestion> ListPasswordQuestions(string languageCode)
        {
            return (FindAllPasswordQuestionOrderedByNameQuery.Invoke(context, languageCode))
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
            var s = FindByIdQuery.Invoke(context, identity);
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