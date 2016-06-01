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
    public class AddNewPage : Popup
    {

        #region Locators
        static readonly By _txtPageName = By.XPath("//input[@id='name']");
        static readonly By _cbbParentPage = By.XPath("//select [@id='parent']");
        static readonly By _cbbNumberOfColumns = By.XPath("//select[@id='columnnumber']");
        static readonly By _cbbDisplayAfter = By.XPath("//select[@id='afterpage']");
        static readonly By _chkPublic = By.XPath("//input[@id='ispublic']");
        #endregion

        #region Elements
        public TextBox TxtPageName
        {
            get { return new TextBox(FindElement(_txtPageName)); }
        }
        public ComboBox CbbParentPage
        {
            get { return new ComboBox(FindElement(_cbbParentPage)); }
        }
        public ComboBox CbbNumberOfColumns
        {
            get { return new ComboBox(FindElement(_cbbNumberOfColumns)); }
        }
        public ComboBox CbbDisplayAfter
        {
            get { return new ComboBox(FindElement(_cbbDisplayAfter)); }
        }
        public Checkbox ChkPublic
        {
            get { return new Checkbox(FindElement(_chkPublic)); }
        }
        #endregion

        #region Methods

        public AddNewPage() { }
        public AddNewPage(IWebDriver webDriver) : base(webDriver) { }

        /// <summary>
        /// Adds the page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2016</date>
        public MainPage AddPage(string pathOfPage, Page page)
        {         
            TxtPageName.SendKeys(page.PageName);
            CbbParentPage.SelectByText(ConvertParentPage(pathOfPage));
            Thread.Sleep(500);
            CbbNumberOfColumns.SelectByText(page.NumberOfColumns.ToString());
            CbbDisplayAfter.SelectByText(page.DisplayAfter);
            if (page.IsPublic)
                ChkPublic.Check();
            else
                ChkPublic.Uncheck();            
            BtnOk.Click();            
            WaitForControlExists(By.XPath(string.Format("//a[.='{0}']", CommonAction.EncodeSpace(page.PageName))));
            return new MainPage(_webDriver);
        }

        /// <summary>
        /// Edits the page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2016</date>
        public MainPage EditPage(string pathOfPage, Page page)
        {            
            TxtPageName.SendKeys(page.PageName);
            CbbParentPage.SelectByText(CommonAction.EncodeSpace(page.ParentPage));
            Thread.Sleep(500);
            CbbNumberOfColumns.SelectByText(page.NumberOfColumns.ToString());
            CbbDisplayAfter.SelectByText(page.DisplayAfter);
            if (page.IsPublic)
                ChkPublic.Check();
            else
                ChkPublic.Uncheck();
            BtnOk.Click();
            //WaitForControlExists(By.XPath(string.Format("//a[.='{0}']", page.PageName)), Constants.WaitTimeoutShortSeconds);
            return new MainPage(_webDriver);
        }

        /// <summary>
        /// Cancels the page.
        /// </summary>
        /// <returns></returns>
        public MainPage CancelPage()
        {
            BtnCancel.Click();
            return new MainPage(_webDriver);
        }

        public string ConvertParentPage(string pathOfPage)
        {
            string fourSpaces = "\u00a0\u00a0\u00a0\u00a0";
            string[] arrNode = pathOfPage.Split('/');
            for (int i = 1; i < arrNode.Length; i++)
            {
                arrNode[arrNode.Length - 1] = fourSpaces + arrNode[arrNode.Length - 1];
            }
            return arrNode[arrNode.Length - 1];

        }
        #endregion


    }
}
