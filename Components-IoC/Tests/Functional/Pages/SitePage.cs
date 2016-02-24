using ReusableLibrary.WatiN;

using Public.FunctionalTests.Parts;

namespace Public.FunctionalTests.Pages
{
    public abstract class SitePage : MasterPage
    {
        protected override void InitializeContents()
        {
            base.InitializeContents();

            HeaderMenu = new HeaderMenuPart(Document);
            QuickLinksMenu = new HeaderQuickLinksMenuPart(Document);
        }

        public HeaderMenuPart HeaderMenu { get; private set; }

        public HeaderQuickLinksMenuPart QuickLinksMenu { get; private set; }
    }
}