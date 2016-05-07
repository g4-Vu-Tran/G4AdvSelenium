using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using SeleniumAdvProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.TestCases
{
    [TestClass]
    public class BaseTestCase
    {
        [TestInitialize]
        public void TestInitializeMethod()
        {
            Console.WriteLine("Run Test Initialize");

            // Start Firefox browser and maximize window

            Constant.WebDriver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), TimeSpan.FromSeconds(300));          
            Constant.WebDriver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void TestCleanupMethod()
        {
            Console.WriteLine("Run Test Cleanup");                     

            // Close browser
            Constant.WebDriver.Quit();
        }                
    }
}
