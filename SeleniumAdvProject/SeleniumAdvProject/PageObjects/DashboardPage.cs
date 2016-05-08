using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumAdvProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.PageObjects
{
    public class DashboardPage : BasePage
    {
        #region Locators

        static readonly By _lblUsername = By.XPath("//a[@href='#Welcome']");
        static readonly By _lblRepository = By.XPath("//a[@href='#Repository']");
        static readonly By _lblCurrentRepository = By.XPath("//a[@href='#Repository']/span");
        static readonly By _lnkLogout = By.XPath("//a[@href='logout.do']");

        #endregion
        #region Elements

        public IWebElement LblUsername
        {
            get { return Constant.WebDriver.FindElement(_lblUsername); }
        }
        public IWebElement LblRepository
        {
            get { return Constant.WebDriver.FindElement(_lblRepository); }
        }
        public IWebElement LblCurrentRepository
        {
            get { return Constant.WebDriver.FindElement(_lblCurrentRepository); }
        }
        public IWebElement LnkLogout
        {
            get { return Constant.WebDriver.FindElement(_lnkLogout); }
        }

        #endregion

        #region Methods
        public string GetUserNameText()
        {
            return LblUsername.Text;
        }

        public string GetCurrentRepositoryText()
        {
            return LblCurrentRepository.Text;
        }

        public LoginPage Logout()
        {
            Actions mouseAction = new Actions(Constant.WebDriver);
            mouseAction.MoveToElement(LblUsername).Perform();
            mouseAction.MoveToElement(LnkLogout).Click().Perform();
            return new LoginPage();
        }

        public DashboardPage SelectRepository(String repositoryName)
        {
            Actions mouseAction = new Actions(Constant.WebDriver);
            mouseAction.MoveToElement(LblRepository).Perform();
            mouseAction.MoveToElement(Constant.WebDriver.FindElement(By.XPath("//ul[@id='ulListRepositories']//a[.='" + repositoryName + "']"))).Click().Perform();
            return this;
        }

        #endregion
    }
}
