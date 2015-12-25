using ReusableLibrary.WatiN;
using WatiN.Core;

namespace Public.FunctionalTests.Parts
{
    public sealed class HeaderQuickLinksMenuPart : Part
    {
        public HeaderQuickLinksMenuPart(IElementContainer container)
            : base(container.Div("logindisplay"))
        {
        }

        public Link LogonLink
        {
            get { return Container.Link(FindHelper.ByPartialUrl("/account/logon")); }
        }

        public Link LogoffLink
        {
            get { return Container.Link(FindHelper.ByPartialUrl("/account/logoff")); }
        }
    }
}
