using System.IO;
using Basin;
using NUnit.Framework;
using Basin.Selenium;

namespace BasinDemo.Tests
{
    public class TestBase
    {
        [SetUp]
        public void SetUp()
        {
            var CONFIG_PATH = Path.GetFullPath("../../../") + "BasinDemo.json";

            BSN.SetConfig(CONFIG_PATH);
            Driver.Init();
            Driver.Goto(BSN.Config.Site.Url);
            Pages.Init();
        }

        [TearDown]
        public void Teardown()
        {
            Driver.Current?.Close();
            Driver.Current?.Quit();
        }
    }
}