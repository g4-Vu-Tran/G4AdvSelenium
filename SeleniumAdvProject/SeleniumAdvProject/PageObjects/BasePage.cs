using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.PageObjects
{
    public class BasePage
    {

        public IWebDriver _webDriver;

        public void SwitchToNewOpenedWindow(IWebDriver driver, bool isNewUrl = true)
        {
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        public void WaitForPageLoadComplete()
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Constants.WaitTimeoutShortSeconds));
            try
            {
                wait.Until(w => ((IJavaScriptExecutor)_webDriver).ExecuteScript("return document.readyState;").Equals("loaded"));
            }
            catch (WebDriverException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void SwitchToNewFrame(IWebElement element)
        {
            _webDriver.SwitchTo().Frame(element);
        }
        public void CloseWindow()
        {
            _webDriver.Close();
        }

        public void WaitForControlExists(By control, int timeoutInSeconds)
        {
            try
            {
                _webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeoutInSeconds));
                IWebElement element = _webDriver.FindElement(control);
                if (element == null)
                {
                    WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
                    wait.Until(drv => drv.FindElement(control));
                }
            }
            catch
            {
                throw new Exception("No element have been found.");

            }
        }
        public void ConfirmDialog(string buttonName)
        {
            switch (buttonName.ToUpper())
            {
                case "OK":
                case "YES":
                    _webDriver.SwitchTo().Alert().Accept();
                    break;

                case "NO":
                case "CANCEL":
                    _webDriver.SwitchTo().Alert().Dismiss();
                    break;
            }
        }
        public string GetDialogText()
        {
            string dglMessage = _webDriver.SwitchTo().Alert().Text;
            return dglMessage;

        }
    }
}
