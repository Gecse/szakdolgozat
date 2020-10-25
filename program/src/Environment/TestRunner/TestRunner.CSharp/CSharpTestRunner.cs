using System.Threading.Tasks;
using HelloCode.Environment.TestRunner.CSharp.Extensions;
using HelloCode.Environment.TestRunner.CSharp.Models;

namespace HelloCode.Environment.TestRunner.CSharp
{
    public class CSharpTestRunner
    {
        /// <summary>
        /// Runs tests for the given project
        /// </summary>
        /// <param name="options">Project options</param>
        /// <returns>Test run results</returns>
        public async Task<TestRun> Run(Options options)
        {
            var compilation = await ProjectCompiler.Compile(options);
            if (compilation.HasErrors())
                return TestRun.FromErrors(compilation.GetErrors().ToTestRunMessage());

            return await CompilationTestRunner.Run(compilation);
        }
    }
}
