using System.IO;
using System.Reflection;

namespace HelloCode.Environment.TestRunner.CSharp
{
    internal static class AssetReader
    {
        public static string Read(string fileName)
        {
            var resourceFilePath = $"{typeof(AssetReader).Namespace}.Assets.{fileName}";
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceFilePath);
            using var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
    }
}
