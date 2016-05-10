using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumAdvProject.Common;
using SeleniumAdvProject.DataObjects;
using OpenQA.Selenium.Support.UI;

namespace SeleniumAdvProject.PageObjects
{
    public class AddNewPage:MainPage
    {
        #region Locators
        static readonly By _txtPageName = By.XPath("//input[@id='name']");
        static readonly By _cbbParentPage = By.XPath("//select [@id='parent']");
        static readonly By _cbbNumberOfColumns = By.XPath("//select[@id='columnnumber']");
        static readonly By _cbbDisplayAfter = By.XPath("//select[@id='afterpage']");
        static readonly By _chkPublic = By.XPath("//input[@id='ispublic']");
        static readonly By _btnOk = By.XPath("//input[@id='OK']");
        static readonly By _btnCancel = By.XPath("//input[@id='Cancel']");
        #endregion

        #region Elements
        public IWebElement TxtPageName
        {
            get { return Constants.WebDriver.FindElement(_txtPageName); }
        }
        public SelectElement CbbParentPage
        {
            get { return new SelectElement(Constants.WebDriver.FindElement(_cbbParentPage)); }
        }
        public SelectElement CbbNumberOfColumns
        {
            get { return new SelectElement(Constants.WebDriver.FindElement(_cbbNumberOfColumns)); }
        }
        public SelectElement CbbDisplayAfter
        {
            get { return new SelectElement(Constants.WebDriver.FindElement(_cbbDisplayAfter)); }
        }
        public IWebElement ChkPublic
        {
            get { return Constants.WebDriver.FindElement(_chkPublic); }
        }
        public IWebElement BtnOk
        {
            get { return Constants.WebDriver.FindElement(_btnOk); }
        }
        public IWebElement BtnCancel
        {
            get { return Constants.WebDriver.FindElement(_btnCancel); }
        }
        #endregion

        #region Methods
        public MainPage addPage(Page page)
        {
            GoToAddNewPage();
            TxtPageName.SendKeys(page.PageName);
            CbbParentPage.SelectByText(page.ParentPage);
            CbbNumberOfColumns.SelectByText(page.NumberOfColumns.ToString());
            CbbDisplayAfter.SelectByText(page.DisplayAfter);
            CbbNumberOfColumns.SelectByText(page.NumberOfColumns.ToString());
            return new MainPage();
        }
        #endregion


    }
}
