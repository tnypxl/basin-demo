using Basin.PageObjects;
using Basin.Selenium;

namespace BasinDemo.PageObjects
{
    public class HomePage : Page<HomePageMap>
    {
        public void NavigateToExample(string linkText)
        {
            Map.ExampleLink(linkText).Click();
        }
    }

    public class HomePageMap : PageMap
    {
        public Element ExampleLink(string linkText) => AnchorTag.WithText(linkText).Inside(ListItemTag);
    }
}