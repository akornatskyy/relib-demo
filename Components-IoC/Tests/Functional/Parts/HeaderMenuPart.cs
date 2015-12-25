using ReusableLibrary.WatiN;
using WatiN.Core;

namespace Public.FunctionalTests.Parts
{
    public sealed class HeaderMenuPart : Part
    {
        public HeaderMenuPart(IElementContainer container)
            : base(container.Div("menucontainer"))
        {
        }

        public Link HomeLink
        {
            get { return Container.Link(FindHelper.ByPartialUrl("/")); }
        }

        public Link TicketsLink
        {
            get { return Container.Link(FindHelper.ByPartialUrl("/tickets")); }
        }

        public Link AboutLink
        {
            get { return Container.Link(FindHelper.ByPartialUrl("/home/about")); }
        }
    }
}
