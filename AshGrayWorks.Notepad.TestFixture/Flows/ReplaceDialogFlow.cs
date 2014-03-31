using System.Text;
using ApprovalTests;
using AshGrayWorks.Notepad.TestFixture.Forms;
using AshGrayWorks.UiTests.Base;
using NUnit.Framework;

namespace AshGrayWorks.Notepad.TestFixture.Flows
{
    public class ReplaceDialogFlow : ChildFlow<MainFlow, ReplaceDialogForm>
    {
        public ReplaceDialogFlow EnterFindWhat(string findWhat)
        {
            Form.FindWhat.Enter(findWhat);

            return this;
        }

        public ReplaceDialogFlow EnterReplaceWith(string replaceWith)
        {
            Form.ReplaceWith.Enter(replaceWith);

            return this;
        }

        public ReplaceDialogFlow ReplaceAll()
        {
            Form.ReplaceAll.Click();

            return this;
        }

        public MainFlow Close()
        {
            Form.Close();

            return Parent;
        }

        public ReplaceDialogFlow VerifyFindNextEnabled(bool enabled)
        {
            Assert.That(Form.FindNext.Enabled, Is.EqualTo(enabled));

            return this;
        }

        public ReplaceDialogFlow VerifyReplaceEnabled(bool enabled)
        {
            Assert.That(Form.Replace.Enabled, Is.EqualTo(enabled));

            return this;
        }

        public ReplaceDialogFlow VerifyReplaceAllEnabled(bool enabled)
        {
            Assert.That(Form.ReplaceAll.Enabled, Is.EqualTo(enabled));

            return this;
        }

        public ReplaceDialogFlow VerifyCancelEnabled(bool enabled)
        {
            Assert.That(Form.Cancel.Enabled, Is.EqualTo(enabled));

            return this;
        }

        public ReplaceDialogFlow RememberActionStatuses(string description)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Replace What: [{0}]\t[Find Next {1}]\r\n", Form.FindWhat.Text, FormatEnabled(Form.FindNext.Enabled));
            sb.AppendFormat("Replace With: [{0}]\t[Replace {1}]\r\n", Form.ReplaceWith.Text, FormatEnabled(Form.Replace.Enabled));
            sb.AppendFormat("                   \t[Replace All {0}]\r\n", FormatEnabled(Form.ReplaceAll.Enabled));
            sb.AppendFormat("                   \t[Cancel: {0}]\r\n", FormatEnabled(Form.Cancel.Enabled));

            ApprovalBucket.Add(description, sb.ToString());

            return this;
        }

        public ReplaceDialogFlow RememberAppearence(string description)
        {
            var appearence = Form.GetAppearence();
            
            Approvals.VerifyBinaryFile(appearence, "bmp");

            return this;
        }

        private string FormatEnabled(bool enabled)
        {
            return enabled ? "O" : "X";
        }
    }
}