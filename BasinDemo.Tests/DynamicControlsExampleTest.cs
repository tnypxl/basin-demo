using System.IO;
using System.Threading;
using System.Reflection.Emit;
using NUnit.Framework;
using Basin.Selenium;

namespace BasinDemo.Tests
{
    public class DynamicControlsExampleTest : TestBase
    {
        [Test]
        public void ShouldRemoveTheCheckbox()
        {
            Pages.Home.LinkFor("Dynamic Controls").Click();
            Assert.That(Pages.DynamicControlsExample.Map.Checkbox.Exists, Is.True);
            Pages.DynamicControlsExample.RemoveCheckbox();
            Assert.That(Pages.DynamicControlsExample.Map.CheckboxExampleMessage.Text.Contains("It's gone!"));
            Assert.That(Pages.DynamicControlsExample.Map.Checkbox.Exists, Is.False);
        }

        [Test]
        public void ShouldEnableTheTextField()
        {
            Pages.Home.LinkFor("Dynamic Controls").Click();
            Assert.That(Pages.DynamicControlsExample.Map.TextField.Enabled, Is.False);
            Pages.DynamicControlsExample.EnableTextField();
            Assert.That(Pages.DynamicControlsExample.Map.TextFieldExampleMessage.Text.Contains("It's enabled!"));
            Assert.That(Pages.DynamicControlsExample.Map.TextField.Enabled, Is.True);
        }
    }
}