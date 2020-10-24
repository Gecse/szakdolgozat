using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace HelloCode.Environment.TestRunner.CSharp.Extensions
{
    internal static class CompilationExtensions
    {
        internal static bool HasErrors(this Compilation compilation)
        {
            return compilation.GetDiagnostics().Any(IsError);
        }

        internal static Diagnostic[] GetErrors(this Compilation compilation)
        {
            return compilation.GetDiagnostics().Where(IsError).ToArray();
        }

        private static bool IsError(Diagnostic diagnostic)
        {
            return diagnostic.Severity == DiagnosticSeverity.Error;
        }

        internal static Assembly ToAssembly(this Compilation compilation)
        {
            using var memoryStream = new MemoryStream();
            var emitResult = compilation.Emit(memoryStream);
            return emitResult.Success ? Assembly.Load(memoryStream.ToArray()) : null;
        }
    }
}
