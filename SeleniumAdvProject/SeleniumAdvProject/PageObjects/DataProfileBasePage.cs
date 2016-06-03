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
    public class DataProfileBasePage : BasePage
    {
        #region Locators
        protected static readonly By _btnNext = By.XPath("//input[@value='Next' and @type='button']");
        protected static readonly By _btnFinish = By.XPath("//input[@value='Finish' and @type='button']");
        protected static readonly By _btnCancel = By.XPath("//input[@value='Cancel' and @type='button']");
        protected static readonly By _btnBack = By.XPath("//input[@value='Back' and @type='button']");
        #endregion

        #region Elements
        public Button BtnNext
        {            get { return new Button(FindElement(_btnNext)); }
        }
        public Button BtnFinish
        {
            get { return new Button(FindElement(_btnFinish)); }
        }
        public Button BtnCancel
        {
            get { return new Button(FindElement(_btnCancel)); }
        }
        public Button BtnBack
        {
            get { return new Button(FindElement(_btnBack)); }
        }
        #endregion

        #region Methods
        public DataProfileBasePage() : base() { }

        public DataProfileBasePage(IWebDriver webDriver) : base(webDriver) { }

        /// <summary>
        /// Click on the button
        /// </summary>
        /// <param name="buttonName">Name of the button</param>
        /// <author>Vu Tran</author>
        /// <date>05/30/2016</date>
        protected void ClickOnButton(string buttonName)
        {
            switch (buttonName)
            {
                case "Back":
                    BtnBack.Click();
                    break;
                case "Next":
                    BtnNext.Click();
                    break;
                case "Finish":
                    BtnFinish.Click();
                    break;
                case "Cancel":
                    BtnCancel.Click();
                    break;
            }
        }

       // enum AddDataProfileSteps { GeneralSettings, DisplayFields, SortFields, FilterFields,StatisticFields, DisplaySubFields, SortSubFields, FilterSubFields};
        #endregion

    }
}
