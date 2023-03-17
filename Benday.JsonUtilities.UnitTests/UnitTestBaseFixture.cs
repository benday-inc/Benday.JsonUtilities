namespace Benday.JsonUtilities.UnitTests;

[TestClass]
public class UnitTestBaseFixture : UnitTestBase
{
    public UnitTestBaseFixture()
    {
    }

    [TestMethod]
    public void CreateSampleConfigFileCreatesFile()
    {
        string path = CreateSampleConfigFile();

        Assert.IsTrue(File.Exists(path), "File didn't exist at '{0}'.", path);

        Assert.AreEqual<string>(UnitTestResources.SampleConfigFile,
            File.ReadAllText(path),
            "Sample config file didn't match original.");
    }
}
