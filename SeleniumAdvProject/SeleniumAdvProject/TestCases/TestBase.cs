using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvProject.Common;
using OpenQA.Selenium.Firefox;

namespace SeleniumAdvProject.TestCases
{
   
    [TestClass]
    public class TestBase
    {
        [TestInitialize]
        public void TestInitializeMethod()
        {
            Console.WriteLine("Run Test Initialize");

            //Start Firefox browser and maximize window
            Constant.WebDriver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), TimeSpan.FromSeconds(200));
            Constant.WebDriver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void TestCleanupMethod()
        {
            Console.WriteLine("Run Test Cleanup");

            //Close browser
            Constant.WebDriver.Quit();
        }
    }
}
