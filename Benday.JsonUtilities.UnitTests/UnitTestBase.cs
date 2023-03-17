using System.Diagnostics;

namespace Benday.JsonUtilities.UnitTests;

public class UnitTestBase
{
    private const string TempFolderName = "Benday.JsonUtilities.UnitTests";

    public TestContext TestContext
    {
        get; set;
    }

    protected string GetArgEntry(string argumentName, string value)
    {
        return String.Format("/{0}:{1}", argumentName, value);
    }

    protected string[] CreateArgsArray(params string[] args)
    {
        return args;
    }

    protected string CreateSampleJsonConfigFileEmpty()
    {
        string filename = "sample-appsettings-empty.json";

        string path =
            Path.Combine(
                Path.GetTempPath(),
                TempFolderName,
                DateTime.UtcNow.Ticks.ToString(),
            TestContext.FullyQualifiedTestClassName,
            TestContext.TestName,
            filename);

        Trace.WriteLine("Path to sample config file '{0}'.", path);

        string dirPath = Path.GetDirectoryName(path);

        if (Directory.Exists(dirPath) == false)
        {
            Directory.CreateDirectory(dirPath);
        }

        File.WriteAllText(path, UnitTestResources.SampleJsonAppSettingsEmpty);

        return path;
    }

    protected string CreateSampleJsonConfigFile()
    {
        string filename = "sample-appsettings.json";

        string path =
            Path.Combine(
                Path.GetTempPath(),
                TempFolderName,
                DateTime.UtcNow.Ticks.ToString(),
            TestContext.FullyQualifiedTestClassName,
            TestContext.TestName,
            filename);

        string dirPath = Path.GetDirectoryName(path);

        if (Directory.Exists(dirPath) == false)
        {
            Directory.CreateDirectory(dirPath);
        }

        File.WriteAllText(path, UnitTestResources.SampleJsonAppSettings);

        return path;
    }

   
    protected string CreateSampleAuthMeFile()
    {
        string filename = "sample-authme.json";

        string path =
            Path.Combine(
                Path.GetTempPath(),
                TempFolderName,
                DateTime.UtcNow.Ticks.ToString(),
            TestContext.FullyQualifiedTestClassName,
            TestContext.TestName,
            filename);

        string dirPath = Path.GetDirectoryName(path);

        if (Directory.Exists(dirPath) == false)
        {
            Directory.CreateDirectory(dirPath);
        }

        File.WriteAllText(path, UnitTestResources.SampleAuthMe);

        return path;
    }

    protected string CreateSampleConfigFile()
    {
        string filename = "sample-config-file.xml";

        string path =
            Path.Combine(
                Path.GetTempPath(),
                TempFolderName,
                DateTime.UtcNow.Ticks.ToString(),
            TestContext.FullyQualifiedTestClassName,
            TestContext.TestName,
            filename);

        Trace.WriteLine("Path to sample config file '{0}'.", path);

        string dirPath = Path.GetDirectoryName(path);

        if (Directory.Exists(dirPath) == false)
        {
            Directory.CreateDirectory(dirPath);
        }

        File.WriteAllText(path, UnitTestResources.SampleConfigFile);

        return path;
    }
}
