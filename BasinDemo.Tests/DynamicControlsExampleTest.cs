using Basin.PageObjects;
using BasinDemo.PageObjects;
using NUnit.Framework;

namespace BasinDemo.Tests
{
    public class DynamicControlsExampleTest : TestBase
    {
        public HomePage Home => Pages.Get<HomePage>();
        public DynamicControlsExamplePage DynamicControlsExample => Pages.Get<DynamicControlsExamplePage>();

        [SetUp]
        public void GoToExample()
        {
            Home.NavigateToExample("Dynamic Controls");
        }

        [Test]
        public void ShouldRemoveTheCheckbox()
        {
            Assert.That(DynamicControlsExample.CheckboxExists(), Is.True);
            DynamicControlsExample.RemoveCheckbox();
            Assert.That(DynamicControlsExample.CheckboxExists(), Is.False);
        }

        [Test]
        public void ShouldEnableTheTextField()
        {
            Assert.That(DynamicControlsExample.TextFieldIsEnabled(), Is.False);
            DynamicControlsExample.EnableTextField();
            Assert.That(DynamicControlsExample.TextFieldIsEnabled(), Is.True);
        }
    }
}