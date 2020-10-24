namespace HelloCode.Environment.TestRunner.CSharp.Models
{
    public class TestOptions
    {
        public string Name { get; }

        public string Directory { get; }

        public TestOptions(string slug, string directory)
        {
            (Name, Directory) = (slug, directory);
        }
    }
}
