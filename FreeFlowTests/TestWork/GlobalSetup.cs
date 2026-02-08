using System;
using System.Xml.Linq;
using System.IO;
using System.Text.Json;


namespace NPlayV1.TestWork
{
    [SetUpFixture]
    public class GlobalSetup
    {
        //[OneTimeSetUp]
        //public void BeforeAll()
        //{
        //    var resultsDir = Path.Combine(TestContext.CurrentContext.TestDirectory, "allure-results");
        //    Directory.CreateDirectory(resultsDir);

        //    var env = new XElement("environment",
        //        new XElement("parameter", new XElement("key", "Platform"), new XElement("value", "GitHub Actions")),
        //        new XElement("parameter", new XElement("key", "Runner"), new XElement("value", "Ubuntu-Latest"))
        //    );
        //    env.Save(Path.Combine(resultsDir, "environment.xml"));
        //}

        [OneTimeSetUp]
        public void InitializeReporting()
        {
            string buildId = Environment.GetEnvironmentVariable("GITHUB_RUN_NUMBER") ?? "Local-Run";
            AllureHelper.GenerateMetadata(buildId);
        }

    }










public static class AllureHelper
    {
        // Dynamically find the allure-results folder inside the bin folder
        private static string ResultsPath =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "allure-results");

        public static void GenerateMetadata(string buildNumber, string envName = "Staging")
        {
            if (!Directory.Exists(ResultsPath)) Directory.CreateDirectory(ResultsPath);

            CreateEnvironmentFile(buildNumber, envName);
            CreateCategoriesFile();
        }

        private static void CreateEnvironmentFile(string build, string env)
        {
            var doc = new XElement("environment",
                new XElement("parameter", new XElement("key", "Build"), new XElement("value", build)),
                new XElement("parameter", new XElement("key", "Environment"), new XElement("value", env)),
                new XElement("parameter", new XElement("key", "Runtime"), new XElement("value", ".NET 8"))
            );
            doc.Save(Path.Combine(ResultsPath, "environment.xml"));
        }

        private static void CreateCategoriesFile()
        {
            var categories = new[] {
            new { name = "Product Defects", matchedStatuses = new[] { "failed" }, messageRegex = ".*Assertion.*" },
            new { name = "System/Infra Issues", matchedStatuses = new[] { "broken" }, messageRegex = ".*Timeout.*|.*Selector.*" }
        };

            File.WriteAllText(Path.Combine(ResultsPath, "categories.json"),JsonSerializer.Serialize(categories)
            );
        }
    }
}
