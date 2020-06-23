using Basin.Pages;
using Basin.Selenium;

namespace BasinDemo.TheInternet.Pages
{
    public class HomePage : Page
    {
        public Element LinkFor(string linkText) => AnchorTag.WithText(linkText);


    }
}