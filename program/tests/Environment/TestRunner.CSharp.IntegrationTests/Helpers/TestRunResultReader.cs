using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using HelloCode.Environment.TestRunner.CSharp.Models;

namespace HelloCode.Environment.TestRunner.CSharp.IntegrationTests.Helpers
{
    internal static class TestRunResultReader
    {
        public static TestRun ReadExpected(TestOptions options)
        {
            return ReadTestRunResult(options, "expected_results.json");
        }

        private static TestRun ReadTestRunResult(TestOptions options, string fileName)
        {
            var testRunResult = DeserializeTestRunResult(options, fileName);

            static int Comparison(TestResult x, TestResult y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            testRunResult.Tests.Sort(Comparison);

            return testRunResult;
        }

        private static TestRun DeserializeTestRunResult(TestOptions solution, string fileName)
        {
            return JsonSerializer.Deserialize<TestRun>(File.ReadAllText(Path.Combine(solution.Directory, fileName)), CreateJsonSerializerOptions());
        }

        private static JsonSerializerOptions CreateJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            options.Converters.Add(new JsonStringEnumConverter());
            return options;
        }
    }
}
