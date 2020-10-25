using HelloCode.Environment.TestRunner.CSharp.IntegrationTests.Helpers;
using HelloCode.Environment.TestRunner.CSharp.Models;
using Shouldly;
using Xunit;

namespace HelloCode.Environment.TestRunner.CSharp.IntegrationTests
{
    public class TestRunnerTests
    {
        [Theory]
        [TestSolutionsData]
        public async void SolutionIsTestedCorrectly(Options options, TestRun expected)
        {
            // Arrange
            var testRunner = new CSharpTestRunner();

            // Act
            var actual = await testRunner.Run(options);

            // Assert
            actual.ShouldBeEquivalentTo(expected);
        }
    }
}
