using System.Collections.Generic;
using System.Diagnostics;
using TestStack.White.UIItems.WindowItems;

namespace AshGrayWorks.UiTests.Base
{
    public abstract class ChildFlow<TParentFlow, TForm> : IFlow
        where TParentFlow : IFlow, new()
        where TForm : FlowForm, new()
    {
        protected TForm Form { get; private set; }
        protected TParentFlow Parent { get; private set; }
        public IDictionary<string, string> ApprovalBucket { get; internal set; }
        private Process Process { get; set; }

        public virtual void InitFlow(IFlow parentFlow, IDictionary<string, string> approvalBucket, Process process, Window window)
        {
            this.Form = FormFactory.CreateForm<TForm>(window);
            this.Parent = (TParentFlow)parentFlow;
            this.Process = process;
            this.ApprovalBucket = approvalBucket;
        }

        protected TChildFlow ChildFlowOnModalWindow<TChildFlow>(string name)
            where TChildFlow : IFlow, new()
        {
            var window = WindowLocator.GetMainWindow(this.Process);
            var modalWindow = WindowLocator.GetModalWindow(window, name);

            return this.CreateFlow<TChildFlow>(this, modalWindow);
        }

        protected T ChildFlowOnMainWindow<T>()
            where T : IFlow, new()
        {
            return this.CreateFlow<T>(this, WindowLocator.GetMainWindow(this.Process));
        }

        protected T SiblingFlowOnMainWindow<T>()
            where T : IFlow, new()
        {
            return this.CreateFlow<T>(this.Parent, WindowLocator.GetMainWindow(this.Process));
        }

        private TChildFlow CreateFlow<TChildFlow>(IFlow parent, Window window)
            where TChildFlow : IFlow, new()
        {
            return FlowFactory.CreateFlow<TChildFlow>(
                parent,
                this.ApprovalBucket,
                this.Process,
                window);
        }
    }
}
