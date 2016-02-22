using System.Collections.Generic;

using ReusableLibrary.Abstractions.Caching;
using ReusableLibrary.Supplemental.Caching;

using Tickets.Interface.Models;

namespace Tickets.Interface.Repository.Caching
{
    public sealed class MembershipRepository : Decorated.MembershipRepository
    {
        public MembershipRepository(IMembershipRepository innerRepository)
            : base(innerRepository)
        {
        }

        public override IEnumerable<PasswordQuestion> ListPasswordQuestions(string languageCode)
        {
            return DefaultCache.Instance.Get(base.ListPasswordQuestions, languageCode);
        }

        public override IDictionary<int, PasswordQuestion> MapPasswordQuestion(string languageCode)
        {
            return DefaultCache.Instance.Get(base.MapPasswordQuestion, languageCode);
        }
    }
}