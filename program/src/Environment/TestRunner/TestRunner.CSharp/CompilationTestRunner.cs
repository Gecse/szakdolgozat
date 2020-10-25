using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HelloCode.Environment.TestRunner.CSharp.Extensions;
using HelloCode.Environment.TestRunner.CSharp.Models;
using Microsoft.CodeAnalysis;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace HelloCode.Environment.TestRunner.CSharp
{
    internal static class CompilationTestRunner
    {
        private static readonly ISourceInformationProvider _sourceInformationProvider = new NullSourceInformationProvider();
        private static readonly TestMessageSink _diagnosticMessageSink = new TestMessageSink();
        private static readonly TestMessageSink _executionMessageSink = new TestMessageSink();

        public static async Task<TestRun> Run(Compilation compilation) =>
            await Run(compilation.Rewrite().ToAssembly().ToAssemblyInfo());

        private static async Task<TestRun> Run(IAssemblyInfo assemblyInfo)
        {
            var testResults = new List<TestResult>();

            _executionMessageSink.Execution.TestFailedEvent += args =>
                testResults.Add(TestResult.FromFailed(args.Message));

            _executionMessageSink.Execution.TestPassedEvent += args =>
                testResults.Add(TestResult.FromPassed(args.Message));

            var testCases = TestCases(assemblyInfo);

            using var assemblyRunner = CreateTestAssemblyRunner(testCases, assemblyInfo.ToTestAssembly());
            await assemblyRunner.RunAsync();

            testResults.Sort(TestResult.Comparison);

            return TestRun.FromTests(testResults);
        }

        private static TestAssembly ToTestAssembly(this IAssemblyInfo assemblyInfo) =>
            new TestAssembly(assemblyInfo);

        private static IReflectionAssemblyInfo ToAssemblyInfo(this Assembly assembly) =>
            Reflector.Wrap(assembly);

        private static XunitTestAssemblyRunner CreateTestAssemblyRunner(IEnumerable<IXunitTestCase> testCases, TestAssembly testAssembly) =>
            new XunitTestAssemblyRunner(
                testAssembly,
                testCases,
                _diagnosticMessageSink,
                _executionMessageSink,
                TestFrameworkOptions.ForExecution()
            );

        private static IXunitTestCase[] TestCases(IAssemblyInfo assemblyInfo)
        {
            using var discoverySink = new TestDiscoverySink();
            using var discoverer = new XunitTestFrameworkDiscoverer(assemblyInfo, _sourceInformationProvider, _diagnosticMessageSink);

            discoverer.Find(false, discoverySink, TestFrameworkOptions.ForDiscovery());
            discoverySink.Finished.WaitOne();

            return discoverySink.TestCases.Cast<IXunitTestCase>().ToArray();
        }
    }
}
