using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace HelloCode.Environment.TestRunner.CSharp.Extensions
{
    internal static class TestRunMessageExtensions
    {
        internal static string ToTestRunMessage(this IEnumerable<string> messages)
        {
            return string.Join("\n", messages);
        }

        internal static string ToTestRunMessage(this Diagnostic[] errors)
        {
            return string.Join("\n", errors.Select(error => error.ToFormattedError()));
        }

        internal static string ToFormattedError(this Diagnostic error)
        {
            return $"{Path.GetFileName(error.Location.SourceTree.FilePath)}:{error.Location.GetLineSpan().StartLinePosition.Line}: {error.GetMessage()}";
        }
    }
}
