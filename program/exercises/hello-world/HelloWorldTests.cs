using Xunit;

public class HelloWorldTests
{
    [Fact]
    public void Say_hi()
    {
        Assert.Equal("Helló, Világ!", HelloWorld.Hello());
    }
}