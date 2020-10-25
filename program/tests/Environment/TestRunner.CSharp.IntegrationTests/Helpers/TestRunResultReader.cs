using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using HelloCode.Environment.TestRunner.CSharp.Models;

namespace HelloCode.Environment.TestRunner.CSharp.IntegrationTests.Helpers
{
    internal static class TestRunResultReader
    {
        public static TestRun ReadExpected(Options options)
        {
            return ReadTestRunResult(options, "expected_results.json");
        }

        private static TestRun ReadTestRunResult(Options options, string fileName)
        {
            var testRunResult = DeserializeTestRunResult(options, fileName);

            testRunResult.Tests.Sort(TestResult.Comparison);

            return testRunResult;
        }

        private static TestRun DeserializeTestRunResult(Options solution, string fileName)
        {
            var text = File.ReadAllText(Path.Combine(solution.Directory, fileName));
            var options = GetJsonSerializerOptions();

            return JsonSerializer.Deserialize<TestRun>(text, options);
        }

        private static JsonSerializerOptions GetJsonSerializerOptions()
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
