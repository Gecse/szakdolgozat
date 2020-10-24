using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HelloCode.Environment.TestRunner.CSharp.Models;
using Humanizer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;

namespace HelloCode.Environment.TestRunner.CSharp
{
    internal static class ProjectCompiler
    {
        public static async Task<Compilation> Compile(TestOptions options)
        {
            var workspace = MSBuildWorkspace.Create();
            var project = await workspace.OpenProjectAsync(GetProjectPath(options));

            return await project
                .AddAdditionalFile("TestBase.cs")
                .WithMetadataReferences(GetMetadataReferences())
                .WithCompilationOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .GetCompilationAsync();
        }

        private static string GetProjectPath(TestOptions options)
        {
            return Path.Combine(options.Directory, $"{options.Name.Humanize().Pascalize()}.csproj");
        }

        private static Project AddAdditionalFile(this Project project, string fileName)
        {
            return project.AddDocument(fileName, AssetReader.Read(fileName)).Project;
        }

        private static IEnumerable<PortableExecutableReference> GetMetadataReferences()
        {
            return AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")
                .ToString()
                .Split(Path.PathSeparator)
                .Select(metadataFilePath => MetadataReference.CreateFromFile(metadataFilePath));
        }
    }
}
