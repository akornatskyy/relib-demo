using System;
using System.Data.Linq;
using System.Linq;
using Tickets.Interface.Models;

namespace Tickets.Module.Repository
{
    public partial class MembershipRepository
    {
        private static readonly Func<TicketsDataContext, string, IQueryable<PasswordQuestion>>
            FindAllPasswordQuestionOrderedByNameQuery = CompiledQuery.Compile<TicketsDataContext, string, IQueryable<PasswordQuestion>>(
                (c, languageCode) => (from q in c.PasswordQuestions
                                      orderby q.Question
                                      select new PasswordQuestion(q.PasswordQuestionId, q.Question)));

        private static readonly Func<TicketsDataContext, int, Entities.Staff>
            FindByIdQuery = CompiledQuery.Compile<TicketsDataContext, int, Entities.Staff>(
                (c, identity) => (from s in c.Staffs
                                  where s.StaffId == identity
                                  select s).Single());
    }
}
