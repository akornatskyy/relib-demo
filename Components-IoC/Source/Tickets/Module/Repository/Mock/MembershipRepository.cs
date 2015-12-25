using System;
using System.Collections;
using System.Collections.Generic;
using ReusableLibrary.Abstractions.Models;
using ReusableLibrary.Abstractions.Repository;
using ReusableLibrary.Supplemental.Collections;
using ReusableLibrary.Supplemental.System;
using Tickets.Interface.Models;
using Tickets.Interface.Repository;

namespace Tickets.Module.Repository.Mock
{
    public sealed class MembershipRepository : IMembershipRepository
    {
        private readonly Random g_random = new Random();
        private readonly IDictionary m_state;

        public MembershipRepository(IDictionary state)
        {
            m_state = state;
            m_state.Add(Key.From<UserCredentials>("demo"), new UserCredentials()
            {
                UserName = "demo",
                Password = "P@ssw0rd"
            });
        }

        #region IMembershipRepository Members

        public void ValidateUser(UserCredentials credentials)
        {
            var key = Key.From<UserCredentials>(credentials.UserName);
            var securityInfo = m_state.GetValue<UserCredentials>(key, null);

            if (securityInfo == null
                || !securityInfo.Password.Equals(credentials.Password, StringComparison.Ordinal))
            {
                throw new RepositoryGuardException(Properties.Resources.GuardValidateUser);
            }
        }

        public void CreateUser(UserSignUpInfo userSignUpInfo)
        {
            var key = Key.From<UserCredentials>(userSignUpInfo.Credentials.UserName);
            m_state.Add(key, userSignUpInfo.Credentials);

            key = Key.From<UserSignUpInfo>(userSignUpInfo.Credentials.UserName);
            m_state.Add(key, userSignUpInfo);
        }

        public IEnumerable<PasswordQuestion> ListPasswordQuestions(string languageCode)
        {
            var list = new List<PasswordQuestion>();
            if (languageCode == "ru")
            {
                list.AddRange(new[] 
                { 
                    new PasswordQuestion(1, "Любимое число"),
                    new PasswordQuestion(2, "Город в котором Вы родились"),
                    new PasswordQuestion(3, "Любимый цвет")
                });
            }
            else
            {
                list.AddRange(new[] 
                { 
                    new PasswordQuestion(1, "Favorite number"),
                    new PasswordQuestion(2, "City of birth"),
                    new PasswordQuestion(3, "Favorite color")
                });
            }

            return list.AsReadOnly();
        }

        public IDictionary<int, PasswordQuestion> MapPasswordQuestion(string languageCode)
        {
            return ListPasswordQuestions(languageCode).ToDictionary<int, PasswordQuestion>();
        }

        #endregion

        #region IRetrieveRepository<int,Staff> Members

        public Staff Retrieve(int identity)
        {
            return NextStaff();
        }

        #endregion

        private Staff NextStaff()
        {
            return new Staff() 
            {
                DisplayName = "{0} {1}".FormatWith(g_random.NextWord().Capitalize(), g_random.NextWord().Capitalize()),
                ReportsTo = g_random.NextInt(1000, 9999),
                StaffId = g_random.NextInt(1000, 9999)
            };
        }
    }
}
