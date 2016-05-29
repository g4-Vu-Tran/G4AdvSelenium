using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvProject.Common;
using SeleniumAdvProject.Ultilities.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.Ultilities
{
    public class Browser
    {
        protected IWebDriver _webDriver;
        protected IWebDriver webDriver
        {
            get { return this._webDriver; }
            set { this._webDriver = value; }
        }

        #region Constructor
        public Browser() { }
        public Browser(IWebDriver webDriver)
        {
            this._webDriver = webDriver;
        }
        #endregion

        #region Method
        /// <summary>
        /// Switches to new opened window.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="isNewUrl">if set to <c>true</c> [is new URL].</param>
        /// <author>Huong Huynh</author>
        public void SwitchToNewOpenedWindow(IWebDriver driver, bool isNewUrl = true)
        {
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        /// <summary>
        /// Waits for page load complete.
        /// </summary>
        /// <author>Huong Huynh</author>
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
        /// <summary>
        /// Switches to new frame.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <author>Huong Huynh</author>
        public void SwitchToNewFrame(IWebElement element)
        {
            _webDriver.SwitchTo().Frame(element);
        }
        /// <summary>
        /// Closes the window.
        /// </summary>
        public void CloseWindow()
        {
            _webDriver.Close();
        }

        /// <summary>
        /// Waits for control exists.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        /// <exception cref="System.Exception">No element have been found.</exception>
        /// <author>Huong Huynh</author>
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

        /// <summary>
        /// Refreshes the current page.
        /// </summary>
        /// <author>Huong Huynh</author>
        public void RefreshCurrentPage()
        {
            _webDriver.Navigate().Refresh();
        }

        /// <summary>
        /// Confirms the dialog.
        /// </summary>
        /// <param name="buttonName">Name of the button.</param>
        /// <author>Huong Huynh</author>
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

        /// <summary>
        /// Gets the dialog text.
        /// </summary>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        public string GetDialogText()
        {
            string dglMessage = _webDriver.SwitchTo().Alert().Text;
            return dglMessage;

        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        public string GetURL()
        {
            string url = _webDriver.Url;
            return url;
        }


        /// <summary>
        /// Clicks the link text.
        /// </summary>
        /// <param name="linkText">The link name which is display on the web app.</param>
        /// <author>
        /// Huong Huynh
        /// </author>
        public void ClickLinkText(string linkText)
        {
            Link lnkDynamic = new Link(_webDriver.FindElement(By.XPath(string.Format("//a[.='{0}']", linkText))));
            lnkDynamic.Click();
        }


        /// <summary>
        /// Determines whether [is text list sorted] [the specified string text].
        /// </summary>
        /// <param name="stringText">The text list.</param>
        /// <param name="sortType">Type of the sort [DESC|ASC].</param>
        /// <returns>bool</returns>
        ///<author>Huong Huynh</author>
        public bool IsTextListSorted(List<string> stringText, string sortType = "ASC")
        {
            int rowCount = stringText.Count;
            bool flag = false;

            // start from 1 to skip the table header row run to 'i < rowCount - 1' because we check
            // a pair of row at a time
            for (int i = 1; i < rowCount - 1; i++)
            {
                if (sortType == "ASC")
                {
                    if (stringText[i].CompareTo(stringText[i + 1]) <= 0)
                        flag = true;
                }
                else
                {
                    if (stringText[i].CompareTo(stringText[i + 1]) >= 0)
                        flag = true;
                }
            }
            return flag;
        }
        #endregion
    }
}
