using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumAdvProject.PageObjects.HomePage
{
    public class MainPage : BasePage
    {
        public MainPage() : base() { }
        public MainPage(IWebDriver webDriver) : base(webDriver) { }
    }
}
