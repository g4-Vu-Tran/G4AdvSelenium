using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvProject.Common;

namespace SeleniumAdvProject.PageObjects
{
    public class LoginPage
    {
        #region Locators
        static readonly By _cbbRepository = By.XPath("//select [@id='repository']");
        static readonly By _txtUsername = By.XPath("//input[@id='username']");
        static readonly By _txtPassword = By.XPath("//input[@id='password']");
        static readonly By _btnLogin = By.XPath("//div[@onclick='Dashboard.login();']");
        
        #endregion

        #region Emlements
        public SelectElement CbbRepository
        {
            get { return new SelectElement(Constant.WebDriver.FindElement(_cbbRepository)); }
        }
        public SelectElement TxtUsername
        {
            get { return new SelectElement(Constant.WebDriver.FindElement(_txtUsername)); }
        }
        public SelectElement TxtPassword
        {
            get { return new SelectElement(Constant.WebDriver.FindElement(_txtPassword)); }
        }
        public SelectElement BtnLogin
        {
            get { return new SelectElement(Constant.WebDriver.FindElement(_btnLogin)); }
        }

        #endregion

        #region Methods
        #endregion
    }
}
