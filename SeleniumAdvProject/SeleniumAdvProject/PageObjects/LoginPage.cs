using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumAdvProject.Common;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAdvProject.PageObjects
{
    public class LoginPage:BasePage
    {
        #region Locators

        static readonly By _ddlRepository = By.XPath("//select[@name='repository']");
        static readonly By _txtUsername = By.XPath("//input[@id='username']");
        static readonly By _txtPassword = By.XPath("//input[@id='password']");
        static readonly By _btnLogin = By.XPath("//div[@class='btn-login']");
        
        #endregion
        #region Elements
        public SelectElement DdlRepsitory
        {
            get { return new SelectElement(Constant.WebDriver.FindElement(_ddlRepository)); }
        }

        public IWebElement TxtUsername
        {
            get { return Constant.WebDriver.FindElement(_txtUsername); }
        }

        public IWebElement TxtPassword
        {
            get { return Constant.WebDriver.FindElement(_txtPassword); }
        }

        public IWebElement BtnLogin
        {
            get { return Constant.WebDriver.FindElement(_btnLogin); }
        }
                
        #endregion

        #region Methods

         public LoginPage Open()
        {
            Constant.WebDriver.Navigate().GoToUrl(Constant.LoginPageUrl);
            return this;
        }
        public DashboardPage Login(string repository,string username, string password)
        {
            DdlRepsitory.SelectByText(repository);
            TxtUsername.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
            DashboardPage dashboardPage = new DashboardPage();
            dashboardPage.WaitForPageLoadComplete();
            dashboardPage.WaitForControlExists(By.XPath("//a[@href='#Welcome']"), Constant.WaitTimeoutShortSeconds);
            return dashboardPage;
        }
        
        public string LoginInValid(string repository, string username, string password)
        {
            DdlRepsitory.SelectByText(repository);
            TxtUsername.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
            string a = "";
            return a;
        }
       
        #endregion

    }
}
