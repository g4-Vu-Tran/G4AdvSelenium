using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvProject.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumAdvProject.Utils;
using SeleniumAdvProject.PageObjects.HomePage;
using SeleniumAdvProject.PageObjects.PanelPage;

namespace SeleniumAdvProject.PageObjects
{
    public class BasePage
    {
        protected IWebDriver _webDriver;

        #region Locators
        protected static readonly By _lblUsername = By.XPath("//a[@href='#Welcome']");
        protected static readonly By _lblRepository = By.XPath("//a[@href='#Repository']");
        protected static readonly By _lblCurrentRepository = By.XPath("//a[@href='#Repository']/span");
        protected static readonly By _lnkLogout = By.XPath("//a[@href='logout.do']");
        protected static readonly By _lblGlobalSetting = By.XPath("//div[@id='main-menu']//li[@class='mn-setting']/a");
        protected static readonly By _lnkAddPage = By.XPath("//a[@class='add' and .='Add Page']");
        protected static readonly By _lnkEditPage = By.XPath("//a[@class='edit' and .='Edit']");
        protected static readonly By _lnkCreateProfile = By.XPath("//a[@class='add' and .='Create Profile']");
        protected static readonly By _lnkCreatePanel = By.XPath("//a[@class='add' and .='Create Panel']");
        protected static readonly By _btnChoosePanel = By.XPath("//a[@id='btnChoosepanel']");
        protected static readonly By _lnkAdminister = By.XPath("//a[.='Administer']");
        protected static readonly By _lnkPanel = By.XPath("//a[.='Panels']");
        protected static readonly By _lnkDataProfiles = By.XPath("//a[.='Data Profiles']");
        protected static readonly By _btnCreateNewPanel = By.XPath("//span[.='Create new panel']");
        protected static readonly By _lnkEditPanel = By.XPath("//li[@class='edit' and @title='Edit Panel']");
        #endregion

        #region Elements
        public IWebElement LblRepository
        {
            get { return FindElement(_lblRepository); }
        }

        public IWebElement LnkEditPanel
        {
            get { return FindElement(_lnkEditPanel); }
        }
        public IWebElement LnkDataProfiles
        {
            get { return FindElement(_lnkDataProfiles); }
        }

        public IWebElement BtnCreateNewPanel
        {
            get { return FindElement(_btnCreateNewPanel); }
        }
        public IWebElement LblCurrentRepository
        {
            get { return FindElement(_lblCurrentRepository); }
        }
        public IWebElement LblGlobalSetting
        {
            get { return FindElement(_lblGlobalSetting); }
        }
        public IWebElement LnkAddPage
        {
            get { return FindElement(_lnkAddPage); }
        }
        public IWebElement LnkCreatePanel
        {
            get { return FindElement(_lnkCreatePanel); }
        }
        public IWebElement LnkEditPage
        {
            get { return FindElement(_lnkEditPage); }
        }

        public IWebElement BtnChoosePanel
        {
            get { return FindElement(_btnChoosePanel); }
        }
        public IWebElement LnkLogout
        {
            get { return FindElement(_lnkLogout); }
        }
        public IWebElement LblUsername
        {
            get { return FindElement(_lblUsername); }
        }
        public IWebElement LnkAdminister
        {
            get { return FindElement(_lnkAdminister); }
        }
        public IWebElement LnkPanel
        {
            get { return FindElement(_lnkPanel); }
        }
        #endregion

        #region Methods
        public BasePage() { }
        public BasePage(IWebDriver webDriver)
        {
            this._webDriver = webDriver;
        }

        public IWebElement FindElement(By by, int waitingTime = 60)
        {
            IWebElement iElement = null;
            while (waitingTime > 0)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                try
                {
                    var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(waitingTime));
                    wait.Until(ExpectedConditions.ElementIsVisible(by));
                    iElement = _webDriver.FindElement(by);
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    waitingTime = stopwatch.Elapsed.Seconds;
                    FindElement(by, waitingTime);
                }
                catch (NullReferenceException)
                {
                    waitingTime = stopwatch.Elapsed.Seconds;
                    FindElement(by, waitingTime);
                }
                stopwatch.Stop();
            }
            return iElement;
        }

        #region Navigate Methods

        /// <summary>
        /// Go to the link follow path of link
        /// </summary>
        /// <param name="pathOfLink">The path to go to the current link</param>
        /// <param name="isClicked">if set to <c>true</c> click on final link</param>
        /// <returns></returns>
        /// <author>Vu Tran</author>
        /// <date>05/25/2016</date>
        public void GoToLink(string pathOfLink, bool isClicked = false)
        {

            string[] arrNode = pathOfLink.Split('/');
            IWebElement LnkName;
            for (int i = 0; i < arrNode.Length; i++)
            {
                LnkName = FindElement(By.XPath(string.Format("//a[.='{0}']", CommonAction.EncodeSpace(arrNode[i]))));
                LnkName.MouseTo();

                if (isClicked && i == arrNode.Length - 1)
                    LnkName.Clicks();
            }
        }

        /// <summary>
        /// Go to the page link follow path of page
        /// </summary>
        /// <param name="pathOfPage">The path to go to the current page</param>
        /// <param name="isClicked">if set to <c>true</c> click on final link</param>
        /// <returns></returns>
        /// <author>Vu Tran</author>
        /// <date>05/25/2016</date>
        public MainPage GoToPage(string pathOfPage)
        {
            GoToLink(pathOfPage, true);
            return new MainPage(_webDriver);
        }

        /// <summary>
        /// Go to the add new page by click on Global Setting > Add Page link
        /// </summary>
        /// <returns>The AddNewPage onject</returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2016</date>
        public AddNewPage GoToAddNewPage()
        {
            LblGlobalSetting.MouseTo();
            LnkAddPage.Click();
            return new AddNewPage(_webDriver);
        }

        /// <summary>
        /// Go to the edit page.
        /// </summary>
        /// <param name="pathOfPage">The path to go to the current page</param>
        /// <returns>The AddNewPage onject</returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2016</date>
        public AddNewPage GoToEditPage(string pathOfPage)
        {
            GoToLink(pathOfPage, true);
            LblGlobalSetting.MouseTo();
            LnkEditPage.Click();
            return new AddNewPage(_webDriver);
        }

        /// <summary>
        /// Go to the panels page.
        /// </summary>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2016</date>
        public PanelsPage GoToPanelsPage()
        {
            LnkAdminister.MouseTo();
            LnkPanel.Clicks();
            return new PanelsPage(_webDriver);
        }

        /// <summary>
        /// Go to the data profiles page.
        /// </summary>
        /// <returns></returns>
        /// <author>Vu Tran</author>
        /// <date>05/25/2016</date>
        public PanelsPage GoToDataProfilesPage()
        {
            LnkAdminister.MouseTo();
            LnkDataProfiles.Clicks();
            return new PanelsPage(_webDriver);
        }

        ///// <summary>
        ///// Go to the add new panel page.
        ///// </summary>
        ///// <returns></returns>
        ///// <author>Huong Huynh</author>
        ///// <date>05/25/2016</date>
        //public AddPanelPage GoToAddPanelPage(OpenAddPanelWay openN)
        //{
        //    LblGlobalSetting.MouseTo();
        //    LnkCreatePanel.Click();
        //    return new AddNewPanelPage(_webDriver);
        //}


        ///// <summary>
        ///// Go to the Data Profile page.
        ///// </summary>
        ///// <returns></returns>
        ///// <author>Vu Tran</author>
        ///// <date>05/25/2016</date>
        //public DataProfilePage GoToDataProfilePage()
        //{
        //    LnkAdminister.MouseOver();
        //    LnkDataProfiles.Click();
        //    return new DataProfilePage(_webDriver);
        //}

        ///// <summary>
        ///// Go to the Data Profile page.
        ///// </summary>
        ///// <returns></returns>
        ///// <author>Vu Tran</author>
        ///// <date>05/25/2016</date>
        //public AddNewPanelPage OpenAddNewPanelPage(string openFrom = "Global setting")
        //{
        //    switch (openFrom)
        //    {
        //        case "Global setting":
        //            {
        //                LblGlobalSetting.MouseOver();
        //                LnkCreatePanel.Click();
        //                break;
        //            }
        //        case "Choose panels":
        //            {
        //                BtnChoosePanel.Click();
        //                BtnCreateNewPanel.Click();
        //                break;
        //            }
        //        case "Panels Page":
        //            {
        //                PanelsPage panelPage = OpenPanelsPage();
        //                panelPage.LnkAddNew.Click();
        //                break;
        //            }
        //    }

        //    return new AddNewPanelPage(_webDriver);
        //}

        ///// <summary>
        ///// Logouts this instance.
        ///// </summary>
        ///// <returns></returns>
        ///// <author>Huong Huynh</author>
        ///// <date>05/25/2016</date>
        //public LoginPage Logout()
        //{
        //    LblUsername.MouseOver();
        //    LnkLogout.Click();
        //    return new LoginPage(_webDriver);
        //}


        ///// <summary>
        ///// Selects the repository on Main Page
        ///// </summary>
        ///// <param name="repositoryName">Name of the repository</param>
        ///// <returns>The MainPage object</returns>
        ///// <author>Vu Tran</author>
        ///// <date>05/26/2016</date>
        //public MainPage SelectRepository(String repositoryName)
        //{
        //    LblRepository.MouseOver();
        //    Link lnkRepository = new Link(_webDriver.FindElement(By.XPath(string.Format("//a[.='{0}']", repositoryName))));
        //    lnkRepository.Click();
        //    return new MainPage(_webDriver);
        //}

        ///// <summary>
        ///// Verify the link is exist
        ///// </summary>
        ///// <param name="linkName">Name of the link.</param>
        ///// <returns></returns>
        ///// <author>Huong Huynh</author>
        ///// <date>05/25/2016</date>
        //public Boolean IsLinkExist(string linkName)
        //{
        //    Link Link = new Link(_webDriver, By.XPath(string.Format("//a[.='{0}']", linkName)));
        //    return Link.isDisplayed();
        //}

        ///// <summary>
        ///// Determines whether [is div exist] [the specified div name].
        ///// </summary>
        ///// <param name="divName">Name of the div.</param>
        ///// <returns></returns>
        ///// Author: Tu Nguyen
        //public Boolean IsDivExist(string divName)
        //{
        //    Div div = new Div(_webDriver, By.XPath(string.Format("//div[@title='{0}']", divName)));
        //    return div.isDisplayed();
        //}

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
        //public void SwitchToNewOpenedWindow(IWebDriver driver, bool isNewUrl = true)
        //{
        //    driver.Close();
        //    driver.SwitchTo().Window(driver.WindowHandles.Last());
        //}

        /// <summary>
        /// Waits for page load complete
        /// </summary>
        /// <return></return>
        /// <author>Huong Huynh</author>
        /// <date>05/26/2015</date>
        //public void WaitForPageLoadComplete()
        //{
        //    WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(Constants.WaitTimeoutShortSeconds));
        //    try
        //    {
        //        wait.Until(w => ((IJavaScriptExecutor)_webDriver).ExecuteScript("return document.readyState;").Equals("loaded"));
        //    }
        //    catch (WebDriverException e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}
        /// <summary>
        /// Switches to new frame
        /// </summary>
        /// <param name="element">The IWebElement</param>
        /// <return></return>
        /// <author>Huong Huynh</author>
        /// <date>05/26/2015</date>
        //public void SwitchToNewFrame(IWebElement element)
        //{
        //    _webDriver.SwitchTo().Frame(element);
        //}
        /// <summary>
        /// Closes the window
        /// </summary>
        /// <return></return>
        /// <author>Huong Huynh</author>
        /// <date>05/26/2015</date>
        //public void CloseWindow()
        //{
        //    _webDriver.Close();
        //}

        /// <summary>
        /// Waits for control exists
        /// </summary>
        /// <param name="control">The By property of element</param>
        /// <param name="timeoutInSeconds">The timeout in seconds</param>
        /// <return></return>
        /// <author>Huong Huynh</author>
        /// <date>05/26/2015</date>
        //public void WaitForControlExists(By control, int timeoutInSeconds = Constants.WaitTimeoutShortSeconds)
        //{
        //    try
        //    {
        //        _webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(timeoutInSeconds));
        //        IWebElement element = _webDriver.FindElement(control);
        //        if (element == null)
        //        {
        //            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
        //            wait.Until(drv => drv.FindElement(control));
        //        }
        //    }
        //    catch
        //    {
        //        throw new Exception("No element have been found.");

        //    }
        //}

        /// <summary>
        /// Refreshes the current page
        /// </summary>
        /// <return></return>
        /// <author>Huong Huynh</author>
        /// <date>05/26/2015</date>
        //public void RefreshCurrentPage()
        //{
        //    _webDriver.Navigate().Refresh();
        //}

        /// <summary>
        /// Confirm OK/YES/NO/CANCEL on the dialog
        /// </summary>
        /// <param name="buttonName">Name of the button on the dialog</param>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        /// <date>05/26/2015</date>
        //public void ConfirmDialog(string buttonName)
        //{
        //    switch (buttonName.ToUpper())
        //    {
        //        case "OK":
        //        case "YES":
        //            _webDriver.SwitchTo().Alert().Accept();
        //            break;

        //        case "NO":
        //        case "CANCEL":
        //            _webDriver.SwitchTo().Alert().Dismiss();
        //            break;
        //    }
        //}

        /// <summary>
        /// Get the text message in the dialog
        /// </summary>
        /// <returns>String</returns>
        /// <author>Huong Huynh</author>
        /// <date>05/26/2015</date>
        //public string GetDialogText()
        //{
        //    string dglMessage = _webDriver.SwitchTo().Alert().Text;
        //    return dglMessage;

        //}

        /// <summary>
        /// Gets the URL
        /// </summary>
        /// <returns>String</returns>
        /// <author>Huong Huynh</author>
        /// <date>05/26/2015</date>
        public string GetURL()
        {
            string url = _webDriver.Url;
            return url;
        }

        //public void ClickLinkText(string linkText)
        //{
        //    Link lnkDynamic = new Link(_webDriver.FindElement(By.XPath(string.Format("//a[.='{0}']", linkText))));
        //    lnkDynamic.Click();
        //}

        //public AddNewPanelPage OpenAddNewPanelPageFromButton()
        //{
        //    BtnChoosePanel.Click();
        //    BtnCreateNewPanel.Click();
        //    return new AddNewPanelPage(_webDriver);
        //}


        public bool isAlertPresent()
        {
            try
            {
                _webDriver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        #endregion
    }
}
