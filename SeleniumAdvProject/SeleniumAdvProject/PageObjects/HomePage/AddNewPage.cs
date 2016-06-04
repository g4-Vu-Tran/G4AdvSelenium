using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumAdvProject.PageObjects.HomePage
{
    public class AddNewPage : BasePage
    {
        public AddNewPage() : base() { }
        public AddNewPage(IWebDriver webDriver) : base(webDriver) { }
    }
}
