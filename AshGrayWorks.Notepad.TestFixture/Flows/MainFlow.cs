using System;
using AshGrayWorks.Notepad.TestFixture.Forms;
using AshGrayWorks.UiTests.Base;
using NUnit.Framework;

namespace AshGrayWorks.Notepad.TestFixture.Flows
{
    public class MainFlow : ChildFlow<NullFlow, MainForm>
    {
        public MainFlow EnterText(string text)
        {
            Form.Editable.Enter(text);

            return this;
        }

        public ReplaceDialogFlow OpenReplaceDialog()
        {
            Form.OpenReplaceDialog();

            return ChildFlowOnModalWindow<ReplaceDialogFlow>("Replace");
        }

        public MainFlow VerifyText(string text)
        {
            Assert.That(Form.Editable.Text, Is.EqualTo(text));

            return this;
        }
    }
}
