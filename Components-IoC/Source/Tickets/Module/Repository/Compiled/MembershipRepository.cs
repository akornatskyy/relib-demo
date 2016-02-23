using System;
using System.Data.Linq;
using System.Linq;

using Tickets.Module.Repository.Entities;

namespace Tickets.Module.Repository
{
    public partial class MembershipRepository
    {
        private static readonly Func<TicketsDataContext, string, IQueryable<Interface.Models.PasswordQuestion>>
            FindAllPasswordQuestionOrderedByNameQuery = CompiledQuery.Compile<TicketsDataContext, string, IQueryable<Interface.Models.PasswordQuestion>>(
                (c, languageCode) => (from q in c.PasswordQuestions
                                      orderby q.Question
                                      select new Interface.Models.PasswordQuestion(q.PasswordQuestionId, q.Question)));

        private static readonly Func<TicketsDataContext, int, Staff>
            FindByIdQuery = CompiledQuery.Compile<TicketsDataContext, int, Staff>(
                (c, identity) => (from s in c.Staffs
                                  where s.StaffId == identity
                                  select s).Single());
    }
}