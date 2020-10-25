namespace HelloCode.Environment.TestRunner.CSharp.Models
{
    public class Options
    {
        public string Name { get; }
        public string Directory { get; }

        public Options(string name, string directory)
        {
            (Name, Directory) = (name, directory);
        }
    }
}
