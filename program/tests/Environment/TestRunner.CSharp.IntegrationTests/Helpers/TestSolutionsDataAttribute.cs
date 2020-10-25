using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace HelloCode.Environment.TestRunner.CSharp.IntegrationTests.Helpers
{
    internal class TestSolutionsDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return TestSolutionsReader.Read().Select(x => new object[] { x.options, x.expected });
        }
    }
}
