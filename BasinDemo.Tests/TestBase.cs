using System.IO;
using NUnit.Framework;
using Basin;
using Basin.Selenium;

namespace BasinDemo.Tests
{
    public class TestBase
    {
        [SetUp]
        public void SetUp()
        {
            var CONFIG_PATH = Path.GetFullPath("../../../") + "BasinDemo.json";

            BasinEnv.SetConfig(CONFIG_PATH);
            Browser.Init();
            Browser.Goto(BasinEnv.Site.Url);
            Pages.Init();
        }

        [TearDown]
        public void Teardown()
        {
            Browser.Current?.Quit();
        }
    }
}