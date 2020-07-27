> _For this demo, I will use [The Internet](http://the-internet.herokuapp.com) as the subject site to write tests against. *The Internet* is a large collection of UI behaviors, patterns, and problem cases encountered while writing browser tests._

## Step 1: Setup

Let's set up a new solution with 2 projects via the command line. The first project will contain the tests and the second will contain the page objects.

```bash
# Create the root of the project and navigate to it

$ mkdir BasinDemo
$ cd BasinDemo

# Create a dotnet solution

$ dotnet new sln

# Use dotnet templates to create a unit test project and a class library project

$ dotnet new nunit -n BasinDemo.Tests --framework netcoreapp3.1
$ dotnet new classlib -n BasinDemo.PageObjects --framework netcoreapp3.1

# Add both projects to the solution

$ dotnet sln BasinDemo.sln add ./BasinDemo.Tests
$ dotnet sln BasinDemo.sln add ./BasinDemo.PageObjects

# Add Selenium WebDriver and Basin Framework dependencies to both projects

$ dotnet add ./BasinDemo.Tests package Selenium.WebDriver --version 3.141.0
$ dotnet add ./BasinDemo.PageObjects package Selenium.WebDriver --version 3.141.0
$ dotnet add ./BasinDemo.Tests package BasinFramework --version 1.2.1
$ dotnet add ./BasinDemo.PageObjects package BasinFramework --version 1.2.1

# Add BasinDemo.PageObjects as a reference in BasinDemo.Tests

$ dotnet add ./BasinDemo.Tests/BasinDemo.Tests.csproj reference ./BasinDemo.PageObjects/BasinDemo.PageObjects.csproj
```

## Step 2: Create a JSON configuration file under the ./Basin.Tests

Create the file

```bash
$ touch BasinDemo.Tests/BasinDemo.json
```

If on Windows...

```powershell
$ New-Item ".\BasinDemo.Tests\BasinDemo.json"
```

Add the following JSON to BasinDemo.json:

```json
{
	"Environment": {
		"Site": "Demo",
		"Browser": "Chrome"
	},
  
	"Sites": [
		{
			"Id": "Demo",
			"Url": "http://the-internet.herokuapp.com"
		}
	],
	
	"Browsers": [
        {
            "Id": "Chrome",
            "Kind": "chrome",
            "Timeout": 10
        }
	]
}
```

## Step 3: Set up and tear down (aka, "test hooks")

Let's make sure any test I write can properly open a browser, navigate to the configured site, and close the browser when the test is finished.

There should be a file named `UnitTest1.cs` under `./BasinDemo.Tests`. Let's rename it to `TestBase.cs`. This will contain all of our test hooks.

*Replace the code inside with:*

```c#
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
            // Bootstrap our json configuration to the Basin environment
            var CONFIG_PATH = Path.GetFullPath("../../../") + "BasinDemo.json";

            BasinEnv.SetConfig(CONFIG_PATH);

			// Instantiates a browser session using the configurating defined in the config
			BrowserSession.Init();

			// Navigates to the site defined in the config
			BrowserSession.Goto(BasinEnv.Site.Url);
        }

        [TearDown]
        public void TearDown()
        {
		    // Closes the browser and kills the WebDriver instance
            BrowserSession.Current?.Quit();
        }
    }
}
```

## Conclusion

With our test framework setup and configured. We are ready to begin building page objects and writing tests. **STAY TUNED FOR PART 2.**

If you want to know more, check out [Basin Framework](https://github.com/tnypxl/BasinFrameworkDotNetCore). Please be aware that this framework has only been in development for a little over two months. APIs can and will change. I will do my best to keep this post updated with those changes.


# PART 2

In part 1, I covered the basic setup of an NUnit test framework that integrates Basin Framework and Selenium WebDriver. For part 2, I will create page objects and add some tests.

## Step 4: Add the Home page class

We need a page class that represents the Home page. The only job this class needs to accomplish is navigating to an example. 

Using the "map" pattern, I store page elements in `HomePageMap` and page behaviors in `HomePage`

```c#
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
```

This makes for clear separations of concern and high maintainability.

## What does `PageMap` and `Page<HomePageMap>` do?

`Page<HomePageMap>` provides a `Map` property containing an instance of `HomePageMap()`.

`PageMap` provides a large set of helper methods for defining and locating elements in the inheriting class.

_Okay, now back on track._

## Step 5: Add the Dynamic Controls page class

Now that I am able to navigate to any example from the Home page, it's time to create page objects for the Dynamic Controls page.

Dynamic Controls performs 4 simple behaviors. Adding and Removing a checkbox input. Enabling and disabling a text input field.

```c#
using Basin.PageObjects;

namespace BasinDemo.PageObjects
{
    public class DynamicControlsExamplePage : Page<DynamicControlExamplePageMap>
    {
        public void RemoveCheckbox() 
        {
            Map.RemoveCheckboxButton.Click();    
        }

        public void AddCheckbox() 
        {
            Map.AddCheckboxButton.Click();
        }

        public void EnableTextField() 
        {
            Map.EnableTextFieldButton.Click();
        }

        public void DisableTextField() 
        {
            Map.DisableTextFieldButton.Click();
        }

        public bool TextFieldIsEnabled() 
        {
            return Map.TextField.Enabled;
        } 

        public bool CheckboxIsDisplayed()
        {
            return Map.Checkbox.Displayed;
        }
    }

    public class DynamicControlsExamplePageMap : PageMap
    {
        private Element AddRemoveButton => ButtonTag.WithAttr("onclick", "swapCheckbox()");

        private Element EnableDisableButton => ButtonTag.WithAttr("onclick", "swapInput()");

        public Element AddCheckboxButton => AddRemoveButton.WithText("Add");

        public Element RemoveCheckboxButton => AddRemoveButton.WithText("Remove"); 

        public Element EnableTextFieldButton => EnableDisableButton.WithText("Enable");
        
        public Element DisableTextFieldButton => EnableDisableButton.WithText("Disable");

        public Element Checkbox => CheckboxInputTag.WithId("checkbox");

        public Element TextField =>  TextInputTag;
    }
}
```

## Step 6: Initialize the page objects.

In `TestBase.cs` in the `SetUp()` method, add the following...

```c#
[SetUp]
public void SetUp()
{
    // ...

    Pages.Add(new HomePage());
    Pages.Add(new DynamicControlsPage());
}
```

`Pages` allows me to initialize instances of page objects and reference them later with `Pages.Get<HomePage>();`.

## Step 7: Okay... now for the tests

Under `./BasinDemo.Tests`, create a file named `DynamicControlsTest.cs`. Add the following...

```c#
using Basin.PageObjects;
using BasinDemo.PageObjects;
using NUnit.Framework;

namespace BasinDemo.Tests
{
    public class DynamicControlsExampleTest : TestBase
    {
        public HomePage Home { get; }

        public DynamicControlsExamplePage DynamicControlsExample { get; }

        public DynamicControlsExampleTest()
        {
            Home = Pages.Get<HomePage>();
            DynamicControlsExample = Pages.Get<DynamicControlsExamplePage>();
        }

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
```

## Step 8: Build, test, pass!

```bash
$ dotnet build
$ dotnet test
```






