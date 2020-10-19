public static class AnalyzerExercise
{
    /* Solution #1
    public static string Hello()
    {
        return "Hello, World!";
    }

    public static string Hello(string name = null)
    {
        return "Hello, " + name + "!";
    }*/

    /* Solution #2*/
    public static string Hello(string name = null)
    {
        return "Hello, " + name ?? "World" + "!";
    }

    /* Solution #3
    public static string Hello(string name = "World")
    {
        return $"Hello {name}!";
    }
    */

    /* Solution #4
    public static string Hello(string name = "World") => $"Hello {name}!";
    */
}