using System;
using BasinDemo.TheInternet.Pages;

namespace BasinDemo.Tests
{
    public static class Pages
    {
        [ThreadStatic] public static HomePage Home;
        [ThreadStatic] public static DynamicControlsExamplePage DynamicControlsExample;

        public static void Init()
        {
            Home = new HomePage();
            DynamicControlsExample = new DynamicControlsExamplePage();
        }
    }
}
