using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
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
        [TestInitialize]
        public IWebDriver _webDriver;
        public void TestInitializeMethod()
        {
            Console.WriteLine("Run Test Initialize");

            DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
            capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
            capabilities.SetCapability(CapabilityType.Version, "44");
            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
            _webDriver = new RemoteWebDriver(new Uri("http://localhost:8888/wd/hub"), capabilities, TimeSpan.FromSeconds(Constants.WaitTimeoutShortSeconds));
            _webDriver.Manage().Window.Maximize();
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
