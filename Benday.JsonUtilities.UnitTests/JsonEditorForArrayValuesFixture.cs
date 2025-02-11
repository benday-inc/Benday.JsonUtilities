using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Benday.JsonUtilities.UnitTests;
[TestClass]
public class JsonEditorForArrayValuesFixture
{
    [TestInitialize]
    public void OnTestInitialize()
    {
        _SystemUnderTest = null;
    }

    private JsonEditor? _SystemUnderTest;

    private JsonEditor SystemUnderTest
    {
        get
        {
            if (_SystemUnderTest == null)
            {
                _SystemUnderTest = new JsonEditor(GetPathToSampleFile());
            }

            return _SystemUnderTest;
        }
    }

    private string GetPathToSampleFile()
    {
        var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        Assert.IsNotNull(dir);

        var pathToFile = Path.Combine(dir, "gab-deploy.json");

        Assert.IsTrue(File.Exists(pathToFile), $"File not found: {pathToFile}");

        return pathToFile;
    }

    [TestMethod]
    public void ReadEnvironmentsArray()
    {
        // arrange
        var expectedCount = 2;

        // act
        var actual = SystemUnderTest.GetArray("environments");

        // assert
        Assert.IsNotNull(actual, "Actual is null.");

        Assert.AreEqual(expectedCount, actual.Count, "Count is wrong.");
    }


}
