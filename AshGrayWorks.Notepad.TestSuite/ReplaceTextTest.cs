using ApprovalTests.Reporters;
using AshGrayWorks.Notepad.TestFixture.Flows;
using AshGrayWorks.UiTests.Base;
using NUnit.Framework;

namespace AshGrayWorks.Notepad.TestSuite
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class ReplaceTextTest : BaseTest<MainFlow>
    {
        [Test]
        public void ReplaceAllShouldReplaceTheFirstWord()
        {
            Start
                .EnterText("lama mila ramu")
                .OpenReplaceDialog()
                    .EnterFindWhat("lama")
                    .EnterReplaceWith("Hacker")
                    .ReplaceAll()
                    .Close()
                .VerifyText("Hacker mila ramu");
        }

        [Test]
        public void ActionsOnReplaceDialogShouldBeEnabledOnlyWhenActionable()
        {
            Start
                .OpenReplaceDialog()
                    .VerifyFindNextEnabled(false)
                    .VerifyReplaceEnabled(false)
                    .VerifyReplaceAllEnabled(false)
                    .VerifyCancelEnabled(true)
                    .EnterFindWhat("a")
                    .VerifyFindNextEnabled(true)
                    .VerifyReplaceEnabled(true)
                    .VerifyReplaceAllEnabled(true)
                    .VerifyCancelEnabled(true)
                    .EnterFindWhat("")
                    .VerifyFindNextEnabled(false)
                    .VerifyReplaceEnabled(false)
                    .VerifyReplaceAllEnabled(false)
                    .VerifyCancelEnabled(true);
        }

        [Test]
        public void ActionsOnReplaceDialogShouldBeEnabledOnlyWhenActionableWithAppovals()
        {
            Start
                .OpenReplaceDialog()
                    .RememberActionStatuses("Initial State")
                    .EnterFindWhat("a")
                    .RememberActionStatuses("'Find What' contains value, so all buttons should be enabled")
                    .EnterFindWhat("")
                    .RememberActionStatuses("When 'Find What' had cleaned, all buttons need to be disabled again");

            Verify();
        }

        [Test]
        [Ignore]
        public void Image()
        {
            Start
                .OpenReplaceDialog()
                    .RememberAppearence("a");
        }
    }
}
