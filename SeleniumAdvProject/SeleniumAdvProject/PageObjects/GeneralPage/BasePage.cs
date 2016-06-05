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
using SeleniumAdvProject.PageObjects.GeneralPage;

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

        /// <summary>
        /// Log out TA Dashboard
        /// </summary>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2016</date>
        public LoginPage Logout()
        {
            LblUsername.MouseTo();
            LnkLogout.Click();
            return new LoginPage(_webDriver);
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

        #endregion

        #region General Methods

        /// <summary>
        /// Get the current userName
        /// </summary>
        /// <returns>the current userName</returns>
        /// <author>Tu Nguyen</author>
        /// <date>05/26/2016</date>
        public string GetUserNameText()
        {
            return LblUsername.Text;
        }

        /// <summary>
        /// Get the current Repository name
        /// </summary>
        /// <returns>the current Repository name</returns>
        /// <author>Vu Tran</author>
        /// <date>05/26/2016</date>
        public string GetCurrentRepositoryText()
        {
            return LblCurrentRepository.Text;
        }

        /// <summary>
        /// Selects the repository
        /// </summary>
        /// <param name="repositoryName">Name of the repository</param>
        /// <returns>The MainPage object</returns>
        /// <author>Vu Tran</author>
        /// <date>05/26/2016</date>
        public MainPage SelectRepository(String repositoryName)
        {
            LblRepository.MouseTo();
            IWebElement lnkRepository = FindElement(By.XPath(string.Format("//a[.='{0}']", repositoryName)));
            lnkRepository.Clicks();
            return new MainPage(_webDriver);
        }

        /// <summary>
        /// Verify the link is exist
        /// </summary>
        /// <param name="linkName">Name of the link.</param>
        /// <returns>True/False</returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2016</date>
        public Boolean IsLinkExist(string linkName)
        {
            IWebElement lnkName = FindElement(By.XPath(string.Format("//a[.='{0}']", linkName)));
            return lnkName.Displayed;
        }

        /// <summary>
        /// Determines whether [is div exist] [the specified div name].
        /// </summary>
        /// <param name="divName">Name of the div.</param>
        /// <returns>True/False</returns>
        /// <author>Tu Nguyen</author>
        /// <date>05/25/2016</date>
        public bool IsDivExist(string divName)
        {
            IWebElement div = FindElement(By.XPath(string.Format("//div[@title='{0}']", divName)));
            return div.Displayed;
        }

        /// <summary>
        /// Waits for page load complete
        /// </summary>
        /// <return></return>
        /// <author>Huong Huynh</author>
        /// <date>05/26/2015</date>
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
        #endregion

        #region Alert methods
        /// <summary>
        /// Confirm OK/CANCEL on the dialog
        /// </summary>
        /// <param name="buttonName">Name of the button on the dialog</param>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        /// <date>05/26/2016</date>
        public void ConfirmDialog(BtnName buttonName)
        {
            switch (buttonName)
            {
                case BtnName.OK:
                    _webDriver.SwitchTo().Alert().Accept();
                    break;
                case BtnName.Cancel:
                    _webDriver.SwitchTo().Alert().Dismiss();
                    break;
            }
        }

        /// <summary>
        /// Get the text message in the dialog
        /// </summary>
        /// <returns>dialog message string</returns>
        /// <author>Huong Huynh</author>
        ///<date>05/26/2016</date>
        public string GetDialogText()
        {
            string dglMessage = _webDriver.SwitchTo().Alert().Text;
            return dglMessage;
        }

        #endregion

        #endregion
    }
}
