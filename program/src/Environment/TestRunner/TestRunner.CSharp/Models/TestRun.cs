using System.Collections.Generic;
using System.Linq;
using HelloCode.Environment.TestRunner.CSharp.Extensions;

namespace HelloCode.Environment.TestRunner.CSharp.Models
{
    /// <summary>
    /// Test run results
    /// </summary>
    public class TestRun
    {
        public string Message { get; }
        public TestStatus Status { get; }
        public List<TestResult> Tests { get; }

        public TestRun(string message, TestStatus status, List<TestResult> tests) =>
            (Message, Status, Tests) =
            (message.NormalizeNewlines(), status, tests);

        internal static TestRun FromErrors(string errors) =>
            new TestRun(errors, TestStatus.Error, new List<TestResult>(0));

        internal static TestRun FromTests(List<TestResult> tests) =>
            new TestRun(null, ToTestStatus(tests), tests);

        private static TestStatus ToTestStatus(List<TestResult> tests)
        {
            if (tests.Any(test => test.Status == TestStatus.Fail))
                return TestStatus.Fail;

            if (tests.All(test => test.Status == TestStatus.Pass) && tests.Count > 0)
                return TestStatus.Pass;

            return TestStatus.Error;
        }
    }
}
