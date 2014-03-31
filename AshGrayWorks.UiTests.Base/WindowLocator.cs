using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;

namespace AshGrayWorks.UiTests.Base
{
    public class WindowLocator
    {
        public static Window GetMainWindow(Process process)
        {
            var application = Application.Attach(process);

            var windows = application.GetWindows();
            Assert.That(windows, Is.Not.Null);

            var wnd = windows[0];
            Assert.IsNotNull(wnd);

            wnd.Focus();

            return wnd;
        }

        public static Window GetModalWindow(Window window, string name)
        {
            Window dialog = null;

            WaitTillCondition(
                () =>
                {
                    dialog = window.ModalWindows().SingleOrDefault(w => w.Name == name);
                    return dialog != null;
                });

            return dialog;
        }

        private static void WaitTillCondition(Func<bool> condition, int attempts = 20, int waits = 20)
        {
            var maxAttempts = attempts;
            var waitInterval = waits;

            while (!condition() && maxAttempts > 0)
            {
                maxAttempts--;
                System.Threading.Thread.Sleep(waitInterval);
            }

            Assert.IsTrue(maxAttempts != 0, "Limit of attempts was exceed!");
        }
    }
}