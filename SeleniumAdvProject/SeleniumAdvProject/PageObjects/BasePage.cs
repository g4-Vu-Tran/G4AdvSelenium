using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumAdvProject.Ultilities.Controls;

namespace SeleniumAdvProject.PageObjects
{
    public class BasePage
    {
        protected IWebDriver _webDriver;

        #region Locators
        static readonly By _lblUsername = By.XPath("//a[@href='#Welcome']");
        static readonly By _lblRepository = By.XPath("//a[@href='#Repository']");
        static readonly By _lblCurrentRepository = By.XPath("//a[@href='#Repository']/span");
        static readonly By _lnkLogout = By.XPath("//a[@href='logout.do']");
        static readonly By _lblGlobalSetting = By.XPath("//li[@class='mn-setting']/a");
        static readonly By _lnkAddPage = By.XPath("//a[@class='add' and .='Add Page']");
        static readonly By _lnkEditMenu = By.XPath("//a[@class='edit' and .='Edit']");
        static readonly By _lnkCreateProfile = By.XPath("//a[@class='add' and .='Create Profile']");
        static readonly By _lnkCreatePanel = By.XPath("//a[@class='add' and .='Create Panel']");
        static readonly By _btnChoosePanel = By.XPath("//a[@id='btnChoosepanel']");
        static readonly By _lnkAdminister = By.XPath("//a[.='Administer']");
        static readonly By _lnkPanel = By.XPath("//a[.='Panels']");
        
        #endregion

        #region Elements
        public Label LblRepository
        {
            get { return new Label(_webDriver.FindElement(_lblRepository)); }
        }
        public Label LblCurrentRepository
        {
            get { return new Label(_webDriver.FindElement(_lblCurrentRepository)); }
        }
        public Label LblGlobalSetting
        {
            get { return new Label(_webDriver.FindElement(_lblGlobalSetting)); }         
        }
        public Link LnkAddPage
        {
            get { return new Link(_webDriver.FindElement(_lnkAddPage)); }
        }
        public Link LnkCreatePanel
        {
            get { return new Link(_webDriver.FindElement(_lnkCreatePanel)); }
        }
        public Link LnkEditMenu
        {
            get { return new Link(_webDriver.FindElement(_lnkEditMenu)); }
        }

        public Button BtnChoosePanel
        {
            get { return new Button(_webDriver.FindElement(_btnChoosePanel)); }
        }
        public Link LnkLogout
        {
            get { return new Link(_webDriver.FindElement(_lnkLogout)); }
        }
        public Link LblUsername
        {
            get { return new Link(_webDriver.FindElement(_lblUsername)); }
        }
        public Link LnkAdminister
        {
            get { return new Link(_webDriver.FindElement(_lnkAdminister)); }
        }
        public Link LnkPanel
        {
            get { return new Link(_webDriver.FindElement(_lnkPanel)); }
        }
        #endregion

        #region Methods

        public BasePage() { }
        public BasePage(IWebDriver webDriver)
        {
            this._webDriver = webDriver;
        }
        
        #region Navigate Methods
        public void ClickMenuItem(string path, bool isClicked = true)
        {
            path = path + "/";
            string node;
            Link LnkName = new Link();
            while (path.IndexOf("/") != -1)
            {
                node = path.Substring(0, path.IndexOf("/"));
                path = path.Substring(node.Length + 1, path.Length - node.Length - 1);

                LnkName = new Link(_webDriver.FindElement(By.XPath(string.Format("//a[.='{0}']", node))));
                LnkName.MouseOver();
            }

            if (isClicked)
            {
                LnkName.Click();
            }
        }

        public MainPage GoToPage(string path, bool isClicked = true)
        {
            ClickMenuItem(path);
            return new MainPage(_webDriver);
        }

        public AddNewPage OpenAddNewPage()
        {
            LblGlobalSetting.MouseOver();
            LnkAddPage.Click();
            return new AddNewPage(_webDriver);
        }
        public AddNewPage OpenEditPage(string pageName)
        {   
            ClickMenuItem(pageName);
            LblGlobalSetting.MouseOver();
            LnkEditMenu.Click();
            return new AddNewPage(_webDriver);
        }
        public AddNewPanelPage OpenAddNewPanelPopup()
        {           
            LblGlobalSetting.MouseOver();
            LnkCreatePanel.Click();
            return new AddNewPanelPage(_webDriver);
        }
        public PanelsPage OpenPanelsPage()
        {
            LnkAdminister.MouseOver();
            LnkPanel.Click();
            return new PanelsPage(_webDriver);
        }
        
        public LoginPage Logout()
        {
            LblUsername.MouseOver();
            LnkLogout.Click();
            return new LoginPage(_webDriver);
        }

        public MainPage SelectRepository(String repositoryName)
        {
            LblRepository.MouseOver();
            Link lnkRepository = new Link(_webDriver.FindElement(By.XPath(string.Format("//a[.='{0}']", repositoryName))));
            lnkRepository.Click();
            return new MainPage(_webDriver);
        }
        public Boolean IsLinkExist(string linkName)
        {
            return _webDriver.FindElement(By.XPath(string.Format("//a[.='{0}']", linkName))).Displayed;
        }
        #endregion

        #region Get Text Methods
        public string GetUserNameText()
        {
            return LblUsername.Text;
        }
        public string GetCurrentRepositoryText()
        {
            return LblCurrentRepository.Text;
        }
        #endregion

        /// <summary>
        /// Switches to new opened window.
        /// </summary>
        /// <param name="driver">The driver.</param>
        /// <param name="isNewUrl">if set to <c>true</c> [is new URL].</param>
        public void SwitchToNewOpenedWindow(IWebDriver driver, bool isNewUrl = true)
        {
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        /// <summary>
        /// Waits for page load complete.
        /// </summary>
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
        public void RefreshCurrentPage()
        {
            _webDriver.Navigate().Refresh();
        }

        /// <summary>
        /// Confirms the dialog.
        /// </summary>
        /// <param name="buttonName">Name of the button.</param>
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
        public string GetDialogText()
        {
            string dglMessage = _webDriver.SwitchTo().Alert().Text;
            return dglMessage;

        }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <returns></returns>
        public string GetURL()
        {
            string url = _webDriver.Url;
            return url;
        }

        public void ClickLinkText(string linkText)
        {
            Link lnkDynamic = new Link(_webDriver.FindElement(By.XPath(string.Format("//a[.='{0}']", linkText))));
            lnkDynamic.Click();
        }
        #endregion
    }
}
