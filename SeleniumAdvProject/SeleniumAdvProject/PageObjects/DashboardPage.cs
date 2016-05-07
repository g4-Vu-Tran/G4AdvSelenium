using OpenQA.Selenium;
using SeleniumAdvProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.PageObjects
{
    public class DashboardPage:BasePage
    {
        #region Locators

        static readonly By _lblUsername = By.XPath("//a[@href='#Welcome']");        

        #endregion
        #region Elements

        public IWebElement LblUsername
        {
            get { return Constant.WebDriver.FindElement(_lblUsername); }
        }

        #endregion

        #region Methods
        public string GetUserNameText()
        {
            return LblUsername.Text;
        }
        


        #endregion
    }
}
