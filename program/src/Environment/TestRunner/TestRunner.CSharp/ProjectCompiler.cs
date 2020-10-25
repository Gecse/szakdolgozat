using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using System.Threading.Tasks;
using HelloCode.Environment.TestRunner.CSharp.Models;
using Humanizer;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;

namespace HelloCode.Environment.TestRunner.CSharp
{
    internal static class ProjectCompiler
    {
        private static readonly IEnumerable<PortableExecutableReference> _metadataReferences;

        static ProjectCompiler()
        {
            _metadataReferences = GetMetadataReferences();

            RegisterMSBuild();
        }

        /// <summary>
        /// Compiles the given project
        /// </summary>
        /// <param name="options">Project options</param>
        /// <returns>Compilation result</returns>
        public static async Task<Compilation> Compile(Options options)
        {
            var workspace = MSBuildWorkspace.Create();
            var project = await workspace.OpenProjectAsync(GetProjectPath(options));

            return await project
                .AddAdditionalFile("TestBase.cs")
                .WithMetadataReferences(_metadataReferences)
                .WithCompilationOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .GetCompilationAsync();
        }

        private static string GetProjectPath(Options options) =>
            Path.Combine(options.Directory, $"{options.Name.Dehumanize().Pascalize()}.csproj");

        private static Project AddAdditionalFile(this Project project, string fileName) =>
            project.AddDocument(fileName, AssetReader.Read(fileName)).Project;

        /// <summary>
        /// Gets system library references
        /// </summary>
        /// <returns>Portable executable references</returns>
        private static IEnumerable<PortableExecutableReference> GetMetadataReferences() =>
            AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")
                .ToString()
                .Split(Path.PathSeparator)
                .Select(metadataFilePath => MetadataReference.CreateFromFile(metadataFilePath));

        /// <summary>
        /// Sets up MSBuild compiler
        /// </summary>
        private static void RegisterMSBuild()
        {
            var vsi = MSBuildLocator.IsRegistered ? MSBuildLocator.QueryVisualStudioInstances().FirstOrDefault() : MSBuildLocator.RegisterDefaults();

            AssemblyLoadContext.Default.Resolving += (assemblyLoadContext, assemblyName) =>
            {
                string path = Path.Combine(vsi.MSBuildPath, assemblyName.Name + ".dll");
                if (File.Exists(path))
                {
                    return assemblyLoadContext.LoadFromAssemblyPath(path);
                }
                return null;
            };
        }
    }
}
