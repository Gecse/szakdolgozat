using HelloCode.Environment.Analyzer.CSharp.Models;

namespace HelloCode.Environment.Analyzer.CSharp.Analyzers.Shared
{
    internal static class SharedComments
    {
        public static readonly SolutionComment HasMainMethod = new SolutionComment("csharp.general.has_main_method");
        public static readonly SolutionComment HasCompileErrors = new SolutionComment("csharp.general.has_compile_errors");
        public static readonly SolutionComment RemoveThrowNotImplementedException = new SolutionComment("csharp.general.remove_throw_not_implemented_exception");
        public static readonly SolutionComment DoNotWriteToConsole = new SolutionComment("csharp.general.do_not_write_to_console");
    }
}