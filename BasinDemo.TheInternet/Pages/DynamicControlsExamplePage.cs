using Basin.Pages;
using Basin.Selenium;

namespace BasinDemo.TheInternet.Pages
{
    public class DynamicControlsExamplePage : Page<DynamicControlsExamplePageMap>
    {
        public DynamicControlsExamplePage RemoveCheckbox()
        {
            Map.RemoveButton.Click();
            Wait.Until(_ => Map.CheckboxExampleMessage.Text.Contains("It's gone!"));
            return this;
        }

        public DynamicControlsExamplePage EnableTextField()
        {
            Map.EnableButton.Click();
            Wait.Until(_ => Map.TextFieldExampleMessage.Text.Contains("It's enabled!"));
            return this;
        }
    }

    public class DynamicControlsExamplePageMap : PageMap
    {
        public Element TextFieldExampleMessage => ParagraphTag.WithId("message")
                                                              .Inside(FormTag.WithId("input-example"));

        public Element CheckboxExampleMessage => ParagraphTag.WithId("message")
                                                             .Inside(FormTag.WithId("checkbox-example"));

        public Element RemoveButton => ButtonTag.WithText("Remove")
                                                .WithAttr("onclick", "swapCheckbox()");

        public Element Checkbox => CheckboxInputTag.WithAttr("label", "blah");

        public Element TextField => TextInputTag.Inside(FormTag.WithId("input-example"));

        public Element EnableButton => ButtonTag.WithText("Enable")
                                                .WithAttr("onclick", "swapInput()");
    }
}