using Basin.Pages;
using Basin.Selenium;

namespace BasinDemo.TheInternet.Pages
{
    public class HomePage : Page
    {
        private Element LinkFor(string linkText) => AnchorTag.WithText(linkText);

        public void NavigateToExample(string exampleName)
        {
            LinkFor(exampleName).Click();
        }
    }
}