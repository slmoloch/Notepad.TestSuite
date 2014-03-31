using ApprovalTests;
using AshGrayWorks.UiTests.Base;
using NUnit.Framework;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowStripControls;

namespace AshGrayWorks.Notepad.TestFixture.Forms
{
    public class MainForm : FlowForm
    {
        [ByAutomationId("15")]
        public TextBox Editable { get; set; }

        public void OpenReplaceDialog()
        {
            var menuBar = Wnd.Get<MenuBar>(SearchCriteria.ByText("Application"));
            var menu = menuBar.TopLevelMenu;
            Assert.That(menu, Is.Not.Null);

            var fileMenu = menu.Find("Edit");
            Assert.That(fileMenu, Is.Not.Null);

            var subMenu = fileMenu.ChildMenus.Find("Replace...");
            Assert.That(subMenu, Is.Not.Null);
            subMenu.Click();
        }
    }
}
