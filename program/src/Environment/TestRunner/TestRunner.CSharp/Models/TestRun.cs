using System;
using System.Collections.Generic;
using System.Linq;
using HelloCode.Environment.TestRunner.CSharp.Extensions;

namespace HelloCode.Environment.TestRunner.CSharp.Models
{
    public class TestRun
    {
        public string Message { get; }
        public TestStatus Status { get; }
        public List<TestResult> Tests { get; }

        public TestRun(string message, TestStatus status, List<TestResult> tests)
        {
            (Message, Status, Tests) = (message.NormalizeNewlines(), status, tests);
        }

        public static TestRun FromErrors(string errors)
        {
            return new TestRun(errors, TestStatus.Error, new List<TestResult>(0));
        }

        public static TestRun FromTests(List<TestResult> tests)
        {
            return new TestRun(null, ToTestStatus(tests), tests);
        }

        private static TestStatus ToTestStatus(IReadOnlyCollection<TestResult> tests)
        {
            if (tests.Any(test => test.Status == TestStatus.Fail))
                return TestStatus.Fail;

            if (tests.All(test => test.Status == TestStatus.Pass) && tests.Any())
                return TestStatus.Pass;

            return TestStatus.Error;
        }
    }
}
