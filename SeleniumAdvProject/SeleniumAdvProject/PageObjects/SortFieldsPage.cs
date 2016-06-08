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
    public class SortFieldsPage : DataProfileBasePage
    {
        #region Locators
        static readonly By _cbbField = By.XPath("//select[@id='cbbFields']");
        static readonly By _btnAddLevel = By.XPath("//input[@id='btnAddSortField']");
        #endregion

        #region Elements
        public ComboBox CbbField
        {
            get { return new ComboBox(FindElement(_cbbField)); }
        }
        public Button BtnAddLevel
        {
            get { return new Button(FindElement(_btnAddLevel)); }
        }
        #endregion

        #region Methods
        public SortFieldsPage() { }
        public SortFieldsPage(IWebDriver webDriver) : base(webDriver) { }

        public bool IsItemTypeListed(string[] expectedItemType)
        {
            IList<string> itemTypeList = CbbField.OptionStrings;
            foreach (string item in expectedItemType)
            {
                if (!itemTypeList.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
