using System;

namespace AshGrayWorks.UiTests.Base
{
    public class ByAutomationIdAttribute : Attribute
    {
        private readonly string id;

        public ByAutomationIdAttribute(string id)
        {
            this.id = id;
        }

        public string Id
        {
            get
            {
                return this.id;
            }
        }
    }
}
