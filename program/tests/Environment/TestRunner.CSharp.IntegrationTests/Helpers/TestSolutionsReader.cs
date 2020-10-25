using System.Collections.Generic;
using System.IO;
using HelloCode.Environment.TestRunner.CSharp.Models;

namespace HelloCode.Environment.TestRunner.CSharp.IntegrationTests.Helpers
{
    internal static class TestSolutionsReader
    {
        public static IEnumerable<(Options options, TestRun expected)> Read()
        {
            foreach (var directory in Directory.GetDirectories("Solutions"))
            {
                var options = new Options("Fake", Path.GetFullPath(directory));
                var expectedTestRunResult = TestRunResultReader.ReadExpected(options);

                yield return (options, expectedTestRunResult);
            }
        }
    }
}
