using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.JsonUtilities.UnitTests;

[TestClass]
public class JsonDevFixture
{

    [TestMethod]
    public void LoadFromString()
    {
        // arrange

        var json = "{ \"FirstLevel\": \"FirstLevelValue\" }";

        // act
        var actual = JsonDocument.Parse(json);

        // assert
        Console.WriteLine(actual.RootElement.ToString());
        Assert.IsNotNull(actual.RootElement, "RootElement was null");
    }


    [TestMethod]
    public void GetValue()
    {
        // arrange

        var json = "{ \"FirstLevel\": \"FirstLevelValue\" }";
        var rootElement = JsonDocument.Parse(json).RootElement;

        var expected = "FirstLevelValue";
        var expectedPropertyName = "FirstLevel";

        // act
        var success = rootElement.TryGetProperty(expectedPropertyName, out var actual);

        // assert
        Assert.IsTrue(success, "Call wasn't successful");
        Assert.AreEqual<string>(expected, actual.GetString()!, "Wrong value");
    }

}
