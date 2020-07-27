using System.IO;
using NUnit.Framework;
using Basin;
using Basin.Selenium;
using Basin.PageObjects;
using BasinDemo.PageObjects;

namespace BasinDemo.Tests
{
    public class TestBase
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var CONFIG_PATH = Path.GetFullPath("../../../") + "BasinDemo.json";

            BasinEnv.SetConfig(CONFIG_PATH);
            Pages.Add(new HomePage());
            Pages.Add(new DynamicControlsExamplePage());
        }

        [SetUp]
        public void SetUp()
        {
            BrowserSession.Init();
            BrowserSession.Goto(BasinEnv.Site.Url);
        }

        [TearDown]
        public void Teardown()
        {
            BrowserSession.Current?.Quit();
        }
    }
}