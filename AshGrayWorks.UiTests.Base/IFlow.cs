using System.Collections.Generic;
using System.Diagnostics;

using TestStack.White.UIItems.WindowItems;

namespace AshGrayWorks.UiTests.Base
{
    public interface IFlow
    {
        void InitFlow(IFlow parentFlow, IDictionary<string, string> approvalBucket, Process process, Window window);
    }
}
