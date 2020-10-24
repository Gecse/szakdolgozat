using HelloCode.Environment.TestRunner.CSharp.Models;

namespace HelloCode.Environment.TestRunner.CSharp.IntegrationTests.Models
{
    public class TestData
    {
        public TestOptions TestOptions { get; private set; }

        public TestRun ExpectedTestRun { get; private set; }

        public TestData(TestOptions testOptions, TestRun expectedTestRun)
        {
            TestOptions = testOptions;
            ExpectedTestRun = expectedTestRun;
        }
    }
}
