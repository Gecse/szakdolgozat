using System.Threading.Tasks;
using HelloCode.Environment.TestRunner.CSharp.Extensions;
using HelloCode.Environment.TestRunner.CSharp.Models;
using Microsoft.Build.Locator;
using Serilog;

namespace HelloCode.Environment.TestRunner.CSharp
{
    public class CSharpTestRunner
    {
        private readonly ILogger _logger;

        public CSharpTestRunner(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TestRun> Run(TestOptions options)
        {
            if (!MSBuildLocator.IsRegistered)
                MSBuildLocator.RegisterDefaults();

            _logger.Information("Running test runner for {Exercise} solution in directory {Directory}", options.Name, options.Directory);

            var compilation = await ProjectCompiler.Compile(options);
            if (compilation.HasErrors())
                return TestRun.FromErrors(compilation.GetErrors().ToTestRunMessage());

            var testRun = await CompilationTestRunner.Run(compilation);

            _logger.Information("Ran test runner for {Exercise} solution in directory {Directory}", options.Name, options.Directory);

            return testRun;
        }
    }
}
