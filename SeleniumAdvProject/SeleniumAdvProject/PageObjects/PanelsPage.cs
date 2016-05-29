using OpenQA.Selenium;
using SeleniumAdvProject.DataObjects;
using SeleniumAdvProject.Ultilities.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumAdvProject.PageObjects
{
    public class PanelsPage : BasePage
    {
        #region Locators
        static readonly By _lnkAddNew = By.XPath("//a[.='Add New']");
        static readonly By _lnkDelete = By.XPath("//a[.='Delete']");
        static readonly By _lnkCheckAll = By.XPath("//a[.='Check All']");
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
        #endregion

        #region Methods

        public PanelsPage() { }
        public PanelsPage(IWebDriver webDriver) : base(webDriver) { }

        /// <summary>
        /// Opens the add new panel popup.
        /// </summary>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public AddNewPanelPage OpenAddNewPanelPopupFromLink()
        {
            LnkAddNew.Click();
            return new AddNewPanelPage(_webDriver);
        }

        /// <summary>
        /// Adds the new panel.
        /// </summary>
        /// <param name="chart">The chart.</param>
        /// <returns></returns>
        /// <author>Huong Huynh</author>
        /// <date>05/25/2015</date>
        public PanelsPage AddNewPanel(Chart chart)
        {
            OpenAddNewPanelPopupFromLink().AddChart(chart);
            return this;
        }

        public PanelsPage AddPanelFromAddNewLink(string from, Chart chart)
        {
            LnkAddNew.Click();
            AddNewPanelPage addNewPanelPage = new AddNewPanelPage();
            addNewPanelPage.AddChart(chart);
            return this;
        }

        #endregion
    }
}
