using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumAdvProject.Common;
using SeleniumAdvProject.DataObjects;
using OpenQA.Selenium.Support.UI;
using SeleniumAdvProject.Ultilities.Controls;
using System.Threading;

namespace SeleniumAdvProject.PageObjects
{
    public class DataProfilePage : BasePage
    {
        #region Locators
        static readonly By _lnkAddNew = By.XPath("//a[.='Add New']");
        static readonly By _lnkDelete = By.XPath("//a[.='Delete']");
        static readonly By _lnkCheckAll = By.XPath("//a[.='Check All']");
        static readonly By _lnkUnCheckAll = By.XPath("//a[.='UnCheck All']");
        #endregion

        #region Elements
        public Link LnkAddNew
        {
            get { return new Link(_webDriver.FindElement(_lnkAddNew)); }
        }
        public Link LnkDelete
        {
            get { return new Link(_webDriver.FindElement(_lnkDelete)); }
        }
        public Link LnkCheckAll
        {
            get { return new Link(_webDriver.FindElement(_lnkCheckAll)); }
        }
        public Link LnkUnCheckAll
        {
            get { return new Link(_webDriver.FindElement(_lnkUnCheckAll)); }
        }
        #endregion

        #region Methods
        public DataProfilePage() : base() { }

        public DataProfilePage(IWebDriver webDriver) : base(webDriver) { }

        /// <summary>
        /// Delete all Data Profiles
        /// </summary>
        /// <returns>DataProfilePage object</returns>
        /// <author>Vu Tran</author>
        /// <date>05/30/2016</date>
        public DataProfilePage DeleteAllDataProfiles()
        {
            LnkCheckAll.Click();
            LnkDelete.Click();
            ConfirmDialog("OK");
            return this;
        }

        /// <summary>
        /// Delete all Data Profiles
        /// </summary>
        /// <returns>DataProfilePage object</returns>
        /// <author>Vu Tran</author>
        /// <date>05/30/2016</date>
        public GeneralSettingsPage GoToGeneralSettingPage()
        {
            LnkAddNew.Click();
            return new GeneralSettingsPage(_webDriver);
        }
        #endregion
    }
}
