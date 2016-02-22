using System;

namespace Tickets.Interface.Models
{
    [Serializable]
    public sealed class UserSignUpInfo
    {
        public UserSignUpInfo()
        {
            Credentials = new UserCredentials();
            Info = new UserInfo();
            Question = PasswordQuestion.None;
        }

        public UserCredentials Credentials { get; set; }

        public UserInfo Info { get; set; }

        public PasswordQuestion Question { get; set; }

        public string Answer { get; set; }
    }
}