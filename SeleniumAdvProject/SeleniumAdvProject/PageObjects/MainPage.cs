using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumAdvProject.Common;
using SeleniumAdvProject.Ultilities.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.PageObjects
{
    public class MainPage : BasePage
    {

        #region Locators
        static readonly By _lnkDelete = By.XPath("//a[@class='delete']");
        #endregion

        #region Elements
        public Link LnkDelete
        {
            get { return new Link(_webDriver.FindElement(_lnkDelete)); }
        }
        #endregion

        #region Methods
        public MainPage(IWebDriver webDriver) : base(webDriver) { }

        public MainPage DeletePage(string path)
        {
            //Link page = new Link();
            //page.FindElement(By.XPath(string.Format("//a[.='{0}']", pageName)));
            //page.Click();
            ClickMenuItem(path);
            LblGlobalSetting.MouseOver();
            LnkDelete.Click();
            return this;
        }

        public int GetPositionPage(string pageName)
        {
            Link page = new Link();
            page.FindElement(By.XPath(string.Format("//a[.='{0}']", pageName)));
            return page.Location.X;

        }

        public MainPage OpenSetting()
        {
            LblGlobalSetting.MouseOver();
            return this;
        }

        public Boolean IsSettingExist()
        {
            bool _isExist = false;
            _isExist = LblGlobalSetting.Exists;
            return _isExist;
        }

        public Boolean IsPageVisible(string pageName)
        {
            Link page = new Link(_webDriver.FindElement(By.XPath(string.Format("//a[.='{0}']", pageName))));
            return page.Enabled;
        }

        #endregion
    }
}
