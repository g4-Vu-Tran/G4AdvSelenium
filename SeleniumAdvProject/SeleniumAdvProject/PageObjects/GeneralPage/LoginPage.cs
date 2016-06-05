using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumAdvProject.Common;
using SeleniumAdvProject.PageObjects.HomePage;
using SeleniumAdvProject.Utils;

namespace SeleniumAdvProject.PageObjects.GeneralPage
{
    public class LoginPage : BasePage
    {
        #region Locators
        static readonly By _ddlRepository = By.XPath("//select[@name='repository']");
        static readonly By _txtUsername = By.XPath("//input[@id='username']");
        static readonly By _txtPassword = By.XPath("//input[@id='password']");
        static readonly By _btnLogin = By.XPath("//div[@class='btn-login']");
        #endregion

        #region Elements
        public IWebElement DdlRepsitory
        {
            get { return FindElement(_ddlRepository); }
        }
        public IWebElement TxtUsername
        {
            get { return FindElement(_txtUsername); }
        }
        public IWebElement TxtPassword
        {
            get { return FindElement(_txtPassword); }
        }
        public IWebElement BtnLogin
        {
            get { return FindElement(_btnLogin); }
        }
        #endregion

        #region Methods

        public LoginPage() : base() { }
        public LoginPage(IWebDriver webDriver) : base(webDriver) { }

        /// <summary>
        /// Opens Login Page
        /// </summary>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public LoginPage Open()
        {
            _webDriver.Navigate().GoToUrl(Constants.LoginPageUrl);
            return this;
        }

        /// <summary>
        /// Logins the dashboard web app.
        /// </summary>
        /// <param name="repository">The repository name</param>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>The MainPage object</returns>
        /// <author>Huong Huynh</author>
        /// <date>05/26/2015</date>
        public MainPage Login(string repository, string username, string password)
        {
            DdlRepsitory.SelectDropDown(repository);
            TxtUsername.EnterText(username);
            TxtPassword.EnterText(password);
            BtnLogin.Click();           
            return new MainPage(_webDriver);
        }

        /// <summary>
        /// Logins dashboard web app with expected error.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns> The error message</returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public string LoginWithExpectedError(string repository, string username, string password)
        {
            DdlRepsitory.SelectDropDown(repository);
            TxtUsername.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
            return this.GetDialogText();
        }

        #endregion

        /// <summary>
        /// Verify Login page is displayed
        /// </summary>
        /// <returns>True/False</returns>
        /// <author>Vu Tran</author>
        /// <date>05/26/2015</date>
        public bool Displayed()
        {
            return BtnLogin.Displayed;
        }

    }
}
