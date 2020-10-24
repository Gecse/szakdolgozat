using HelloCode.Environment.TestRunner.CSharp.Extensions;
using Xunit.Abstractions;

namespace HelloCode.Environment.TestRunner.CSharp.Models
{
    public class TestResult
    {
        public string Name { get; }
        public string Message { get; }
        public string Output { get; }
        public TestStatus Status { get; }

        public TestResult(string name, TestStatus status, string message, string output)
        {
            (Name, Message, Status, Output) = (name.NormalizeNewlines(), message.NormalizeNewlines(), status, output.NormalizeNewlines());
        }

        internal static TestResult FromPassed(ITestPassed test)
        {
            return new TestResult(test.TestCase.DisplayName, TestStatus.Pass, null, test.Output);
        }

        internal static TestResult FromFailed(ITestFailed test)
        {
            return new TestResult(test.TestCase.DisplayName, TestStatus.Fail, test.Messages.ToTestRunMessage(), test.Output);
        }
    }
}
