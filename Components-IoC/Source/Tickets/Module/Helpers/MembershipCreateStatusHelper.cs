using System.Web.Security;

using Tickets.Module.Properties;

namespace Tickets.Module.Helpers
{
    internal static class MembershipCreateStatusHelper
    {
        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://msdn.microsoft.com/en-us/library/system.web.security.membershipcreatestatus.aspx for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return MembershipCreateStatusNames.DuplicateUserName;

                case MembershipCreateStatus.DuplicateEmail:
                    return MembershipCreateStatusNames.DuplicateEmail;

                case MembershipCreateStatus.InvalidPassword:
                    return MembershipCreateStatusNames.InvalidPassword;

                case MembershipCreateStatus.InvalidEmail:
                    return MembershipCreateStatusNames.InvalidEmail;

                case MembershipCreateStatus.InvalidAnswer:
                    return MembershipCreateStatusNames.InvalidAnswer;

                case MembershipCreateStatus.InvalidQuestion:
                    return MembershipCreateStatusNames.InvalidQuestion;

                case MembershipCreateStatus.InvalidUserName:
                    return MembershipCreateStatusNames.InvalidUserName;

                case MembershipCreateStatus.ProviderError:
                    return MembershipCreateStatusNames.ProviderError;

                case MembershipCreateStatus.UserRejected:
                    return MembershipCreateStatusNames.UserRejected;

                default:
                    return MembershipCreateStatusNames.UnknownError;
            }
        }
    }
}