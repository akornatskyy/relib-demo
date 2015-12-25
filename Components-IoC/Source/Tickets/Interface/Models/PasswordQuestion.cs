using System;
using ReusableLibrary.Abstractions.Models;

namespace Tickets.Interface.Models
{
    [Serializable]
    public sealed class PasswordQuestion : ValueObject<int>
    {
        public PasswordQuestion(int key, string displayName)
            : base(key, displayName)
        {
        }

        public static readonly PasswordQuestion None = new PasswordQuestion(0, "None");
    }
}
