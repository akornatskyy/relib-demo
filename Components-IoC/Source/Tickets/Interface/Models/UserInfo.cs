using System;

namespace Tickets.Interface.Models
{
    [Serializable]
    public sealed class UserInfo
    {
        public string Email { get; set; }

        public string DisplayName { get; set; }
    }
}