using System;
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
        private static readonly ISourceInformationProvider SourceInformationProvider = new NullSourceInformationProvider();
        private static readonly TestMessageSink DiagnosticMessageSink = new TestMessageSink();
        private static readonly TestMessageSink ExecutionMessageSink = new TestMessageSink();

        public static async Task<TestRun> Run(Compilation compilation)
        {
            return await Run(compilation.Rewrite().ToAssembly().ToAssemblyInfo());
        }

        private static async Task<TestRun> Run(IAssemblyInfo assemblyInfo)
        {
            var testResults = new List<TestResult>();

            ExecutionMessageSink.Execution.TestFailedEvent += args =>
            {
                testResults.Add(TestResult.FromFailed(args.Message));
            };

            ExecutionMessageSink.Execution.TestPassedEvent += args =>
            {
                testResults.Add(TestResult.FromPassed(args.Message));
            };

            var testCases = TestCases(assemblyInfo);

            using var assemblyRunner = CreateTestAssemblyRunner(testCases, assemblyInfo.ToTestAssembly());
            await assemblyRunner.RunAsync();

            static int Comparison(TestResult x, TestResult y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            testResults.Sort(Comparison);

            return TestRun.FromTests(testResults);
        }

        private static TestAssembly ToTestAssembly(this IAssemblyInfo assemblyInfo)
        {
            return new TestAssembly(assemblyInfo);
        }

        private static IReflectionAssemblyInfo ToAssemblyInfo(this Assembly assembly)
        {
            return Reflector.Wrap(assembly);
        }

        private static XunitTestAssemblyRunner CreateTestAssemblyRunner(IEnumerable<IXunitTestCase> testCases, TestAssembly testAssembly)
        {
            return new XunitTestAssemblyRunner(
                testAssembly,
                testCases,
                DiagnosticMessageSink,
                ExecutionMessageSink,
                TestFrameworkOptions.ForExecution());
        }

        private static IXunitTestCase[] TestCases(IAssemblyInfo assemblyInfo)
        {
            using var discoverySink = new TestDiscoverySink();
            using var discoverer = new XunitTestFrameworkDiscoverer(assemblyInfo, SourceInformationProvider, DiagnosticMessageSink);

            discoverer.Find(false, discoverySink, TestFrameworkOptions.ForDiscovery());
            discoverySink.Finished.WaitOne();

            return discoverySink.TestCases.Cast<IXunitTestCase>().ToArray();
        }
    }
}
