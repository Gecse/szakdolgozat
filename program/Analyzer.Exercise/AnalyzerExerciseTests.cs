using Xunit;

public class AnalyzerExerciseTests
{
    [Fact]
    public void Hello_WithoutName_UsesDefault()
    {
        Assert.Equal("Hello, World!", AnalyzerExercise.Hello());
    }

    [Fact]
    public void Hello_WithName_UsesName()
    {
        Assert.Equal("Hello, Jack!", AnalyzerExercise.Hello("Jack"));
    }
}

