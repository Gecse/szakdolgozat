using HelloCode.Environment.TestRunner.CSharp.Extensions;
using Xunit.Abstractions;

namespace HelloCode.Environment.TestRunner.CSharp.Models
{
    /// <summary>
    /// Single test case result
    /// </summary>
    public class TestResult
    {
        public string Name { get; }
        public string Message { get; }
        public string Output { get; }
        public TestStatus Status { get; }

        public TestResult(string name, TestStatus status, string message, string output) =>
            (Name, Message, Status, Output) =
            (name.NormalizeNewlines(), message.NormalizeNewlines(), status, output.NormalizeNewlines());

        internal static TestResult FromPassed(ITestPassed test) =>
            new TestResult(test.TestCase.DisplayName, TestStatus.Pass, null, test.Output);

        internal static TestResult FromFailed(ITestFailed test) =>
            new TestResult(test.TestCase.DisplayName, TestStatus.Fail, test.Messages.ToTestRunMessage(), test.Output);

        public static int Comparison(TestResult x, TestResult y) =>
            string.CompareOrdinal(x.Name, y.Name);
    }
}
