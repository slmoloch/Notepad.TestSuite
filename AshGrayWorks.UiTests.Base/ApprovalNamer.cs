using ApprovalTests.Core;
using ApprovalTests.Namers;

namespace AshGrayWorks.UiTests.Base
{
    internal class ApprovalNamer : IApprovalNamer
    {
        private readonly IApprovalNamer namer;

        public ApprovalNamer()
        {
            this.namer = new UnitTestFrameworkNamer();
        }

        public string Name
        {
            get
            {
                return this.namer.Name.Split('.')[1];
            }
        }

        public string SourcePath
        {
            get
            {
                return this.namer.SourcePath + @"\" + this.namer.Name.Split('.')[0];
            }
        }
    }
}