using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ApprovalTests;
using NUnit.Framework;
using TestStack.White;

namespace AshGrayWorks.UiTests.Base
{
    public class Ignition : IDisposable
    {
        private IDictionary<string, string> approvalBucket;
        private Process process;

        public TStartFlow Start<TStartFlow>()
            where TStartFlow : IFlow, new()
        {
            process = Process.Start(ConfigurationManager.AppSettings["ExecutablePath"]);
            approvalBucket = new Dictionary<string, string>();

            return FlowFactory.CreateRootFlow<TStartFlow>(process, approvalBucket);
        }

        public void Verify()
        {
            if (approvalBucket.Any())
            {
                Approvals.Verify(ConvertDictionaryToStringBuilder(approvalBucket));

                approvalBucket.Clear();
            }
        }

        public void Dispose()
        {
            if (null == process)
            {
                return;
            }

            if (!process.WaitForExit(1000))
            {
                process.Kill();
            }

            process.Dispose();

            if (approvalBucket.Any())
            {
                Assert.Fail("Call Verify in test body.");
            }
        }

        private static string ConvertDictionaryToStringBuilder(IEnumerable<KeyValuePair<string, string>> dict)
        {
            var builder = new StringBuilder();

            foreach (var line in dict)
            {
                builder.AppendLine(line.Key + " =");
                builder.AppendLine("{");

                if (!string.IsNullOrEmpty(line.Value))
                {
                    builder.AppendLine(line.Value);
                }

                builder.AppendLine("}");
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
