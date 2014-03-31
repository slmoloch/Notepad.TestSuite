using System.Collections.Generic;
using System.Diagnostics;
using TestStack.White.UIItems.WindowItems;

namespace AshGrayWorks.UiTests.Base
{
    public class FlowFactory
    {
        public static TChildFlow CreateFlow<TChildFlow>(IFlow parent, IDictionary<string, string> approvalBucket, Process process, Window window)
            where TChildFlow : IFlow, new()
        {
            var child = new TChildFlow();
            child.InitFlow(parent, approvalBucket, process, window);
            return child;
        }

        public static T CreateRootFlow<T>(Process process, IDictionary<string, string> approvalBucket)
            where T : IFlow, new()
        {
            var mainWindow = WindowLocator.GetMainWindow(process);
            return CreateFlow<T>(new NullFlow(), approvalBucket, process, mainWindow);
        }
    }
}
