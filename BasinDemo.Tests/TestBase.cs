using System.IO;
using Basin;
using NUnit.Framework;
using Basin.Selenium;
using Basin.Selenium.Builders;
using Basin.Config.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace BasinDemo.Tests
{
    public class TestBase
    {
        [SetUp]
        public void SetUp()
        {
            var CONFIG_PATH = Path.GetFullPath("../../../") + "BasinDemo.json";

            BSN.SetConfig(CONFIG_PATH);

            Driver.Init(() =>
            {
                var builder = new FirefoxBuilder(new DriverConfig()
                {
                    Headless = true
                });

                builder.DriverOptions.PageLoadStrategy = PageLoadStrategy.Eager;
                builder.DriverOptions.LogLevel = FirefoxDriverLogLevel.Debug;

                return builder;
            });

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