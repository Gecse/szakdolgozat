using System.Collections.Generic;
using System.IO;
using HelloCode.Environment.TestRunner.CSharp.IntegrationTests.Models;
using HelloCode.Environment.TestRunner.CSharp.Models;

namespace HelloCode.Environment.TestRunner.CSharp.IntegrationTests.Helpers
{
    internal static class TestSolutionsReader
    {
        public static List<TestData> ReadAll()
        {
            var output = new List<TestData>();

            foreach (var directory in Directory.GetDirectories("Solutions"))
            {
                var options = new TestOptions("Fake", Path.GetFullPath(directory));
                var expectedTestRun = TestRunResultReader.ReadExpected(options);

                output.Add(new TestData(options, expectedTestRun));
            }

            return output;
        }
    }
}
