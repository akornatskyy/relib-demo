using System;
using System.Web.Security;

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
                    return Properties.MembershipCreateStatusNames.DuplicateUserName;

                case MembershipCreateStatus.DuplicateEmail:
                    return Properties.MembershipCreateStatusNames.DuplicateEmail;

                case MembershipCreateStatus.InvalidPassword:
                    return Properties.MembershipCreateStatusNames.InvalidPassword;

                case MembershipCreateStatus.InvalidEmail:
                    return Properties.MembershipCreateStatusNames.InvalidEmail;

                case MembershipCreateStatus.InvalidAnswer:
                    return Properties.MembershipCreateStatusNames.InvalidAnswer;

                case MembershipCreateStatus.InvalidQuestion:
                    return Properties.MembershipCreateStatusNames.InvalidQuestion;

                case MembershipCreateStatus.InvalidUserName:
                    return Properties.MembershipCreateStatusNames.InvalidUserName;

                case MembershipCreateStatus.ProviderError:
                    return Properties.MembershipCreateStatusNames.ProviderError;

                case MembershipCreateStatus.UserRejected:
                    return Properties.MembershipCreateStatusNames.UserRejected;

                default:
                    return Properties.MembershipCreateStatusNames.UnknownError;
            }
        }
    }
}
