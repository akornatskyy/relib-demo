using System;

namespace Tickets.Interface.Models
{
    [Serializable]
    public sealed class UserCredentials
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}