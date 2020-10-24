using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using HelloCode.Environment.TestRunner.CSharp.IntegrationTests.Helpers;
using HelloCode.Environment.TestRunner.CSharp.IntegrationTests.Models;
using Serilog;
using Xunit;

namespace HelloCode.Environment.TestRunner.CSharp.IntegrationTests
{
    public class TestRunnerTests
    {
        [Theory]
        [TestSolutionsData]
        public async void SolutionIsTestedCorrectly(TestData data)
        {
            // Arrange
            var testRunner = new CSharpTestRunner(Log.Logger);
            var expected = data.ExpectedTestRun;
            // Act
            var actual = await testRunner.Run(data.TestOptions);

            // Assert
            // Assert.Equal(data.ExpectedTestRun, actual);
            Assert.Equal(expected.Message, actual.Message);
            Assert.Equal(expected.Status, actual.Status);
            Assert.Equal(expected.Tests.Count, actual.Tests.Count);
            for (int i = 0; i < actual.Tests.Count; i++)
            {
                Assert.Equal(expected.Tests[i].Name, actual.Tests[i].Name);
                Assert.Equal(expected.Tests[i].Message, actual.Tests[i].Message);
                Assert.Equal(expected.Tests[i].Output, actual.Tests[i].Output);
                Assert.Equal(expected.Tests[i].Status, actual.Tests[i].Status);
            }
        }
    }
}
