using NUnit.Framework;

namespace BasinDemo.Tests
{
    public class DynamicControlsExampleTest : TestBase
    {
        [SetUp]
        public void GoToExample()
        {
            Pages.Home.NavigateToExample("Dynamic Controls");
        }

        [Test]
        public void ShouldRemoveTheCheckbox()
        {
            Assert.That(Pages.DynamicControlsExample.Map.Checkbox.Exists, Is.True);
            Pages.DynamicControlsExample.RemoveCheckbox();
            Assert.That(Pages.DynamicControlsExample.Map.Checkbox.Exists, Is.False);
        }

        [Test]
        public void ShouldEnableTheTextField()
        {
            Assert.That(Pages.DynamicControlsExample.Map.TextField.Enabled, Is.False);
            Pages.DynamicControlsExample.EnableTextField();
            Assert.That(Pages.DynamicControlsExample.Map.TextField.Enabled, Is.True);
        }
    }
}