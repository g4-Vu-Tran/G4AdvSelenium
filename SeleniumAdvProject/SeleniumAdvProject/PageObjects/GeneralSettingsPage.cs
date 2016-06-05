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
    public class GeneralSettingsPage : DataProfileBasePage
    {
        #region Locators
        static readonly By _txtProfileName = By.XPath("//input[@id='txtProfileName']");
        static readonly By _cbbItemType = By.XPath("//select[@id='cbbEntityType']");
        static readonly By _cbbRelatedData = By.XPath("//select[@id='cbbSubReport']");
        #endregion

        #region Elements
        public TextBox TxtProfileName
        {
            get { return new TextBox(FindElement(_txtProfileName)); }
        }
        public ComboBox CbbItemType
        {
            get { return new ComboBox(FindElement(_cbbItemType)); }
        }
        public ComboBox CbbRelatedData
        {
            get { return new ComboBox(FindElement(_cbbRelatedData)); }
        }
        #endregion

        #region Methods
        public GeneralSettingsPage() : base() { }

        public GeneralSettingsPage(IWebDriver webDriver) : base(webDriver) { }

        public object SetGeneralSettingsValue(string name, string itemType, string relatedData, string button = "Next")
        {

            TxtProfileName.SendKeys(name);
            CbbItemType.SelectByText(itemType);
            CbbRelatedData.SelectByText(relatedData);

            switch (button)
            {
                case "Finish":
                    {
                        BtnFinish.Click();
                        if (isAlertPresent())
                        {
                            return this;
                        }
                        return new DataProfilePage(_webDriver);
                    }
                case "Cancel": BtnCancel.Click();
                    return new DataProfilePage(_webDriver);
                default: BtnNext.Click();
                    return new DisplayFieldsPage(_webDriver);
            }
        }

        #endregion

    }
}
