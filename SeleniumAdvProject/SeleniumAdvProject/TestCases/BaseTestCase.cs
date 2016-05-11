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
        public void TestInitializeMethod()
        {
            Console.WriteLine("Run Test Initialize");

            DesiredCapabilities capabilities = DesiredCapabilities.Firefox();
            capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
            capabilities.SetCapability(CapabilityType.Version, "30");
            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));

 //                       Constants.WebDriver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), TimeSpan.FromSeconds(300));          
            RemoteWebDriver _rmWebDriver = new RemoteWebDriver(new Uri("http://localhost:8888/wd/hub"), 
                capabilities,
                TimeSpan.FromSeconds(Constants.WaitTimeoutShortSeconds));
                // Start Firefox browser and maximize window

            //onstants.WebDriver = new FirefoxDriver(new FirefoxBinary(), new FirefoxProfile(), TimeSpan.FromSeconds(300));          

            Constants.WebDriver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void TestCleanupMethod()
        {
            Console.WriteLine("Run Test Cleanup");                     

            // Close browser
            Constants.WebDriver.Quit();
        }                
    }
}
