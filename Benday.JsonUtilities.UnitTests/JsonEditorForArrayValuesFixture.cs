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
        var actual = SystemUnderTest.Root.GetArray("environments");

        // assert
        Assert.IsNotNull(actual, "Actual is null.");

        Assert.AreEqual(expectedCount, actual.Count, "Count is wrong.");
    }

    [TestMethod]
    public void GetChildByAttributeValue_ById_FirstItem()
    {
        // arrange
        var expectedId = "3";
        var expectedName = "test";

        // act
        var actual = SystemUnderTest.Root.GetArrayItem(
            "environments", "id", expectedId);

        // assert
        Assert.IsNotNull(actual, "Actual is null.");


        var actualId = actual.GetString("id");
        var actualName = actual.GetString("name");

        Assert.AreEqual(expectedId, actualId, "Id is wrong.");
        Assert.AreEqual(expectedName, actualName, "Name is wrong.");
    }

    [TestMethod]
    public void GetChildByAttributeValue_ById_SecondItem()
    {
        // arrange
        var expectedId = "4";
        var expectedName = "production";

        // act
        var actual = SystemUnderTest.Root.GetArrayItem(
            "environments", "id", expectedId);

        // assert
        Assert.IsNotNull(actual, "Actual is null.");

        var actualId = actual.GetString("id");
        var actualName = actual.GetString("name");

        Assert.AreEqual(expectedId, actualId, "Id is wrong.");
        Assert.AreEqual(expectedName, actualName, "Name is wrong.");
    }

    [TestMethod]
    public void GetQueueId()
    {
        var expectedQueueId = 461;

        var environment = SystemUnderTest.Root.GetArrayItem(
            "environments", "id", "3");

        var deployPhases = environment.GetArray("deployPhases");

        var deploymentInput = deployPhases.FirstOrDefaultWithPropertyName("deploymentInput");

        var queueId = deploymentInput?.GetInt32("queueId");

        Assert.AreEqual(expectedQueueId, queueId, "QueueId is wrong.");
    }

}
