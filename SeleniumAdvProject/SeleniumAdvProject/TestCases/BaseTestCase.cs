using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using SeleniumAdvProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.TestCases
{
    [TestClass]
    public class BaseTestCase
    {
        protected IWebDriver _webDriver;

        [TestInitialize]
        public void TestInitializeMethod()
        {
            Console.WriteLine("Run Test Initialize");

            DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
            capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
            capabilities.SetCapability(CapabilityType.Version, "46.0.1");
            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
            _webDriver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), capabilities, TimeSpan.FromSeconds(300));

           //_webDriver = new FirefoxDriver();
            _webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            _webDriver.Manage().Window.Maximize();
            _webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Constants.WaitTimeoutShortSeconds));          
        }



        [TestCleanup]
        
        public void TestCleanupMethod()
        {
            Console.WriteLine("Run Test Cleanup");

            // Close browser
            _webDriver.Quit();
        }
    }
}
