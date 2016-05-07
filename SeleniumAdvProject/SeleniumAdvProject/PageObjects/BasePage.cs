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
        public void SwitchToNewOpenedWindow(IWebDriver driver, bool isNewUrl = true)
        {
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }
       
        public void WaitForPageLoadComplete()
        {
            WebDriverWait wait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(Constant.WaitTimeoutShortSeconds));
            try
            {
                wait.Until(w => ((IJavaScriptExecutor)Constant.WebDriver).ExecuteScript("return document.readyState;").Equals("loaded"));
            }
            catch (WebDriverException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        
        public void SwitchToNewFrame(IWebElement element)
        {
            Constant.WebDriver.SwitchTo().Frame(element);
        }
        public void CloseWindow()
        {
            Constant.WebDriver.Close();
        }

        public void WaitForControlExists(By control, int timeoutInSeconds)
        {
            try
            {
                Constant.WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeoutInSeconds));
                IWebElement element = Constant.WebDriver.FindElement(control);
                if (element == null)
                {
                    WebDriverWait wait = new WebDriverWait(Constant.WebDriver, TimeSpan.FromSeconds(timeoutInSeconds));
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
                    Constant.WebDriver.SwitchTo().Alert().Accept();
                    break;

                case "NO":
                case "CANCEL":
                    Constant.WebDriver.SwitchTo().Alert().Dismiss();
                    break;
            }
        }
        public string GetDialogText()
        {
            string dglMessage= Constant.WebDriver.SwitchTo().Alert().Text;
            return dglMessage;

        }
    }
}
